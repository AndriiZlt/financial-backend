using aspnetcore.ntier.BLL.Services.IServices;
using aspnetcore.ntier.DAL.Entities;
using aspnetcore.ntier.DAL.Repositories.IRepositories;
using aspnetcore.ntier.DTO.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Security.Claims;



namespace aspnetcore.ntier.BLL.Services
{
    public class TransactionService : ITransactionService
    {

        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IStockService _stockService;
        private readonly IBoardService _boardService;


        public TransactionService(
            ITransactionRepository transactionRepository,
            IMapper mapper,
            IHttpContextAccessor httpContext,
            IStockService stockService,
            IBoardService boardService
            )
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
            _httpContext = httpContext;
            _stockService = stockService;
            _boardService = boardService;
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
            if (userId != transactionFrontend.Seller_User_Id.ToString() && userId != transactionFrontend.Buyer_User_Id.ToString())
            {
                Log.Error("Security restriction: Only seller or buyer can add transaction");
                throw new UnauthorizedAccessException();
            }

            var transactionToAdd = _mapper.Map<Transaction>(transactionFrontend);

            /*Make sure we have anough stocks to sell*/
            StockDTO stockToUpdate = await _stockService.GetStockAsync(transactionFrontend.Stock_Id);
            stockToUpdate.Qty = (Int32.Parse(stockToUpdate.Qty) - Int32.Parse(transactionFrontend.Qty)).ToString();
            stockToUpdate.Status = StockStatus.Fixed;
            if (Int32.Parse(stockToUpdate.Qty) < 0)
            {
                Log.Error("The Stock's quantity is not anough (stock id={stokID})", stockToUpdate.Id);
                throw new ArgumentOutOfRangeException();
            }

            var addedTransaction = await _transactionRepository.AddAsync(transactionToAdd);

            /* Creating/updating Stock for Buyer */
            StockToAddDTO stockToAdd = _mapper.Map<StockToAddDTO>(transactionFrontend);
            stockToAdd.User_Id = transactionFrontend.Buyer_User_Id;
            stockToAdd.Cost_Basis = transactionFrontend.Price;
            stockToAdd.Status = StockStatus.Fixed;
            await _stockService.AddStockAsync(stockToAdd);

            /* Updating Stock of Seller */
            await _stockService.UpdateStockAsync(transactionFrontend.Stock_Id, stockToUpdate);

            /*Deleting BoardItem*/
            await _boardService.DeleteBoardItemAsync(transactionFrontend.Stock_Id);

            return _mapper.Map<TransactionDTO>(addedTransaction);
        }
    }
}
