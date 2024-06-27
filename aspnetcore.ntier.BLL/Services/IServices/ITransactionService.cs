using aspnetcore.ntier.DTO.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace aspnetcore.ntier.BLL.Services.IServices
{
    public interface ITransactionService
    {

        Task<List<TransactionDTO>> GetTransactionsAsync(CancellationToken cancellationToken = default);
        Task<TransactionDTO> AddTransactionAsync([FromBody] TransactionFrontendDTO transactionToAdd);
    }
}