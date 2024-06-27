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


        public TransactionService(
            ITransactionRepository transactionRepository,
            IMapper mapper,
            IHttpContextAccessor httpContext
            )
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
            _httpContext = httpContext;

        }

        public async Task<List<TransactionDTO>> GetTransactionsAsync(CancellationToken cancellationToken = default)
        {
            var userId = _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var transactionsToReturn = await _transactionRepository.GetListAsync(Int32.Parse(userId));
            return _mapper.Map<List<TransactionDTO>>(transactionsToReturn);
        }


        public async Task<TransactionDTO> AddTransactionAsync([FromBody] TransactionFrontendDTO transactionFrontend)
        {
            Log.Information("Transaction from Frontend: {@transaction}", transactionFrontend);

            var userId = _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != transactionFrontend.Selling_User_Id.ToString() && userId != transactionFrontend.Buying_User_Id.ToString())
            {
                Log.Error("Security restriction: Only seller or buyer can add transaction");
                throw new UnauthorizedAccessException();
            }

            var transactionToAdd = _mapper.Map<TransactionToAddDTO>(transactionFrontend);

            if (userId == transactionFrontend.Selling_User_Id.ToString())
            {
                transactionToAdd.User_Id = transactionFrontend.Selling_User_Id;
                transactionToAdd.Other_Side_User_Id = transactionFrontend.Buying_User_Id;
                transactionToAdd.Side = TransactionSide.Seller;
            }

            if (userId == transactionFrontend.Buying_User_Id.ToString())
            {
                transactionToAdd.User_Id = transactionFrontend.Buying_User_Id;
                transactionToAdd.Other_Side_User_Id = transactionFrontend.Selling_User_Id;
                transactionToAdd.Side = TransactionSide.Buyer;
            }

            Log.Information("TransactionToAdd: {@transaction}", transactionToAdd);
            var transaction = _mapper.Map<Transaction>(transactionToAdd);
            Log.Information("Transaction: {@transaction}", transaction);
            var addedTransaction = await _transactionRepository.AddAsync(transaction);
            return _mapper.Map<TransactionDTO>(addedTransaction);
        }
    }
}
