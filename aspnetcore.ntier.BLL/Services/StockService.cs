using aspnetcore.ntier.BLL.Services.IServices;
using aspnetcore.ntier.DAL.Entities;
using aspnetcore.ntier.DAL.Repositories;
using aspnetcore.ntier.DAL.Repositories.IRepositories;
using aspnetcore.ntier.DTO.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading.Tasks;



namespace aspnetcore.ntier.BLL.Services
{
    public class StockService : IStockService
    {

        private readonly IStockRepository _stockRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;


        public StockService (
            IStockRepository stockRepository, 
            IMapper mapper, 
            IHttpContextAccessor httpContext 
            )
        {
            _stockRepository = stockRepository;
            _mapper = mapper;
            _httpContext = httpContext;

        }

        public async Task<List<StockDTO>> GetStocksAsync(CancellationToken cancellationToken = default)
        {
            var userId = _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var stocksToReturn = await _stockRepository.GetListAsync(Int32.Parse(userId));
            Log.Information("GetStocksAsync {@stockId}", userId);
            return _mapper.Map<List<StockDTO>>(stocksToReturn);
        }

        public async Task<List<StockDTO>> GetBoardAsync(CancellationToken cancellationToken = default)
        {
            var stocksToReturn = await _stockRepository.GetBoardListAsync();
            return _mapper.Map<List<StockDTO>>(stocksToReturn);
        }

        public async Task<StockDTO> AddStockAsync([FromBody] StockToAddDTO stockToAddDTO)
        {
            Log.Information("Stock to ADD: {@stockId}", stockToAddDTO);
            var userId = _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var stockToUpdate = await _stockRepository.GetAsync(x => (x.Alpaca_Asset_Id == stockToAddDTO.Alpaca_Asset_Id && x.User_Id.ToString()== userId));
            if (stockToUpdate == null)
            {
                stockToAddDTO.User_Id = Int32.Parse(userId);
                var stock = _mapper.Map<Stock>(stockToAddDTO);
                var addedStock = await _stockRepository.AddAsync(_mapper.Map<Stock>(stockToAddDTO));
                Log.Information("Added Stock: {@stockId}", addedStock);
                return _mapper.Map<StockDTO>(addedStock);
            }
            else
            {
                stockToUpdate.Qty = (Int32.Parse(stockToUpdate.Qty) + Int32.Parse(stockToAddDTO.Qty)).ToString();
                var updatedStock = await _stockRepository.UpdateAsync(_mapper.Map<Stock>(stockToUpdate));
                Log.Information("Stock updated: {@stockId}", updatedStock);
                return _mapper.Map<StockDTO>(updatedStock);

            }



        }

 

/*        public async Task DeleteTaskAsync(int taskId)
        {
            var taskToDelete = await _stockRepository.GetAsync(x => x.Id == taskId);

            if (taskToDelete is null)
            {
                Log.Information("Task with taskId = {TaskId} was not found", taskId);
                throw new KeyNotFoundException();
            }

            await _stockRepository.DeleteAsync(taskToDelete);
        }*/

        public async Task<StockDTO> UpdateStatusAsync(int stockId, StockStatus status)
        {
            Log.Information("UpdateStatus: {Id},{status}", stockId, status);
            var stockToUpdate = await _stockRepository.GetAsync(x => x.Id == stockId);
            if (stockToUpdate is null)
            {
                Log.Information("Stock with Id = {Id} was not found", stockId);
                throw new KeyNotFoundException();
            }

            stockToUpdate.Status = status;

            var stock = _mapper.Map<Stock>(stockToUpdate);

            Log.Information("Stock {@stock} has been updated", stockToUpdate);

            return _mapper.Map<StockDTO>(await _stockRepository.UpdateAsync(stock));
        }

        public async Task<StockDTO> BuyStockAsync(string stockId)
        {

/* 1. 
 * Check if user has already this stock
 if yes - update it's quantity and 



 if not - create stock 
 
 
 */
            var stockToUpdate = await _stockRepository.GetAsync(x => x.Id.ToString() == stockId);
            if (stockToUpdate is null)
            {
                Log.Information("Stock with Id = {Id} was not found", stockId);
                throw new KeyNotFoundException();
            }
            var userId = _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            stockToUpdate.User_Id = Int32.Parse(userId);
            stockToUpdate.Status = StockStatus.Fixed;
            var stock = _mapper.Map<Stock>(stockToUpdate);

            Log.Information("Updated stock before saving: {@stock}", stockToUpdate);

            return _mapper.Map<StockDTO>(await _stockRepository.BuyStockAsync(stock));
        }

        public Task DeleteTaskAsync(int taskId)
        {
            throw new NotImplementedException();
        }

        public Task<StockDTO> UpdateStockAsync(StockDTO stock)
        {
            throw new NotImplementedException();
        }

        /*public async Task<StockDTO> BuyStockAsync(StockDTO updatedStock)
        {
            var stockToUpdate = await _stockRepository.GetAsync(x => x.Id == updatedStock.Id);

            if (stockToUpdate is null)
            {
                Log.Information("Stock with Id = {TaskId} was not found", updatedStock.Id);
                throw new KeyNotFoundException();
            }

            var stockAfterUpdate = _mapper.Map<Stock>(updatedStock);

            Log.Information("Task with these properties: {@TaskToUpdate} has been updated", updatedStock);

            return _mapper.Map<StockDTO>(await _stockRepository.UpdateStockAsync(stockAfterUpdate));
        }*/




    }
}
