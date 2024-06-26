﻿using aspnetcore.ntier.BLL.Services.IServices;
using aspnetcore.ntier.DAL.DataContext;
using aspnetcore.ntier.DAL.Entities;
using aspnetcore.ntier.DAL.Repositories.IRepositories;
using aspnetcore.ntier.DTO.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Security.Claims;
using System.Transactions;



namespace aspnetcore.ntier.BLL.Services
{
    public class TransactionService : ITransactionService
    {

        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IStockService _stockService;
        private readonly IBoardService _boardService;
        private readonly IUserService _userService;
        private readonly AspNetCoreNTierDbContext _aspNetCoreNTierDbContext;


        public TransactionService(
            ITransactionRepository transactionRepository,
            IMapper mapper,
            IHttpContextAccessor httpContext,
            IStockService stockService,
            IBoardService boardService,
            IUserService userService,
            AspNetCoreNTierDbContext aspNetCoreNTierDbContext
            )
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
            _httpContext = httpContext;
            _stockService = stockService;
            _boardService = boardService;
            _userService = userService;
            _aspNetCoreNTierDbContext = aspNetCoreNTierDbContext;
        }

        public async Task<List<TransactionDTO>> GetTransactionsAsync(CancellationToken cancellationToken = default)
        {
            var userId = _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var transactionsToReturn = await _transactionRepository.GetListAsync(Int32.Parse(userId));
            return _mapper.Map<List<TransactionDTO>>(transactionsToReturn);
        }


        public async Task<TransactionDTO> AddTransactionAsync([FromBody] TransactionToAddDTO transactionFrontend)
        {

            var userId = _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            /* Pre-transaction checks */

            /* Only buyer or seller can create transaction */
            if (userId != transactionFrontend.Seller_User_Id.ToString() && userId != transactionFrontend.Buyer_User_Id.ToString())
            {
                Log.Error("Security restriction: Only seller or buyer can add transaction");
                throw new UnauthorizedAccessException();
            }

            /*Checking buyer's ballance*/
            var buyerBallance= await this._userService.GetUserBallanceAsync(transactionFrontend.Buyer_User_Id);
            if (buyerBallance < float.Parse(transactionFrontend.Price)) 
            {
                Log.Error($"Insufficient buyer's ballance! Ballance:{buyerBallance} < Price:{transactionFrontend.Price} ");
                throw new ArgumentOutOfRangeException();
            }

            /*Checking seller's stock and quantity*/
            var sellerStocks = await _stockService.GetStocksAsync(transactionFrontend.Seller_User_Id);
            var stockForSale = sellerStocks.FirstOrDefault(s=>s.Symbol == transactionFrontend.Symbol);
            if (stockForSale != null)
            {
                if (Int32.Parse(stockForSale.Qty) < Int32.Parse(transactionFrontend.Qty))
                {
                    Log.Error($"Insufficient stock quantity! Qty:{stockForSale.Qty} < Needed:{transactionFrontend.Qty} ");
                    throw new ArgumentOutOfRangeException();
                } 
            } else
            {
                Log.Error($"No stock to sell!");
                throw new KeyNotFoundException();
            }
            /*----End of checks----*/

            /*----Starting transaction----*/
            var transaction = _aspNetCoreNTierDbContext.Database.BeginTransaction();

            try
            {
                /* Creating/updating the buyer stock */
                StockToAddDTO stockToAdd = _mapper.Map<StockToAddDTO>(transactionFrontend);
                stockToAdd.User_Id = transactionFrontend.Buyer_User_Id;
                stockToAdd.Cost_Basis = transactionFrontend.Price;
                stockToAdd.Status = StockStatus.Fixed;
                Log.Information("New updated data for Buyer stock: {@stock}", stockToAdd);
                var newBuyerStock = _stockService.UpdateOrAddStockAsync(stockToAdd);

                /* Updating the seller stock */
                StockDTO stockForUpdate = _mapper.Map<StockDTO>(stockForSale);
                stockForUpdate.Qty = (Int32.Parse(stockForUpdate.Qty) - Int32.Parse(transactionFrontend.Qty)).ToString();
                stockForUpdate.Status = StockStatus.Fixed;
                Log.Information("Updated Seller stock: {@stock}", stockForUpdate);
                var updatedStock = await _stockService.UpdateStockAsync(transactionFrontend.Seller_Stock_Id, stockForUpdate);

                /* Deleting the BoardItem */
                await _boardService.DeleteBoardItemAsync(transactionFrontend.Board_Item_Id);

                /* Updating seller's and buyer's ballances */
                var sellerBallance = await _userService.GetUserBallanceAsync(transactionFrontend.Seller_User_Id);
                var newSellerBallance = await _userService.UpdateUserBallanceAsync(transactionFrontend.Seller_User_Id, sellerBallance + float.Parse(transactionFrontend.Price));
                var newBuyerBallance = await _userService.UpdateUserBallanceAsync(transactionFrontend.Buyer_User_Id, buyerBallance - float.Parse(transactionFrontend.Price));

                /* Creating transaction */
                var transactionToAdd = _mapper.Map<DAL.Entities.Transaction>(transactionFrontend);
                transactionToAdd.Buyer_Stock_Id = newBuyerStock.Id;
                var addedTransaction = await _transactionRepository.AddAsync(transactionToAdd);

                transaction.Commit();

                return _mapper.Map<TransactionDTO>(addedTransaction);
            }
            catch (Exception ex) 
            {
                Log.Error("Error accured while preforming transaction", ex.Message);
                throw new TransactionAbortedException();
            }

        }
    }
}
