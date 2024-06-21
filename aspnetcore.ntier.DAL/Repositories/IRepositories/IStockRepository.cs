﻿using aspnetcore.ntier.DAL.Entities;
using System.Linq.Expressions;

namespace aspnetcore.ntier.DAL.Repositories.IRepositories
{
    public interface IStockRepository 
    {
        Task<Stock> GetAsync(Expression<Func<Stock, bool>> filter = null, CancellationToken cancellationToken = default);

        Task<List<Stock>> GetListAsync(int userId);

        Task<Stock> AddAsync(Stock stock);

        Task<int> DeleteAsync(Stock stockk);

        Task<Stock> UpdateStatusTaskAsync(Stock stock);

        Task<Stock> UpdateStockAsync(Stock stock);

    }
}