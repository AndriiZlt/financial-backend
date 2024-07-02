using aspnetcore.ntier.BLL.Services.IServices;
using aspnetcore.ntier.BLL.Utilities.CustomExceptions;
using aspnetcore.ntier.DAL.DataContext;
using aspnetcore.ntier.DAL.Entities;
using aspnetcore.ntier.DAL.Repositories;
using aspnetcore.ntier.DAL.Repositories.IRepositories;
using aspnetcore.ntier.DTO.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Security.Claims;



namespace aspnetcore.ntier.BLL.Services
{
    public class StockService : IStockService
    {

        private readonly IStockRepository _stockRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;
        private readonly AspNetCoreNTierDbContext _aspNetCoreNTierDbContext;


        public StockService (
            IStockRepository stockRepository, 
            IMapper mapper, 
            IHttpContextAccessor httpContext, AspNetCoreNTierDbContext aspNetCoreNTierDbContext
            )
        {
            _stockRepository = stockRepository;
            _mapper = mapper;
            _httpContext = httpContext;
            _aspNetCoreNTierDbContext = aspNetCoreNTierDbContext;
        }

        public async Task<List<StockDTO>> GetStocksAsync(CancellationToken cancellationToken = default)
        {
            var userId = _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var stocksToReturn = await _stockRepository.GetListAsync(Int32.Parse(userId));
            Log.Information("GetStocksAsync {@stockId}", userId);
            return _mapper.Map<List<StockDTO>>(stocksToReturn);
        }

        public async Task<List<StockDTO>> GetStocksAsync(int userId,  CancellationToken cancellationToken = default)
        {
            var stocksToReturn = await _stockRepository.GetListAsync(userId);
            Log.Information("GetStocksAsync {@stockId}", userId);
            return _mapper.Map<List<StockDTO>>(stocksToReturn);
        }

        public async Task<StockDTO> GetStockAsync(int stockId, CancellationToken cancellationToken = default)
        {

            var stockToReturn = await _stockRepository.GetAsync(x => x.Id == stockId, cancellationToken);

            if (stockToReturn is null)
            {
                Log.Error("Stock with Id = {UserId} was not found", stockId);
                throw new KeyNotFoundException();
            }

            return _mapper.Map<StockDTO>(stockToReturn);
        }

        public async Task<StockDTO> AddStockAsync([FromBody] StockToAddDTO stockToAddDTO)
        {
            Log.Information("Stock to ADD: {@stockId}", stockToAddDTO);
            var userId = _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var stockToUpdate = await _stockRepository.GetAsync(x => (x.Symbol == stockToAddDTO.Symbol && x.User_Id.ToString()== userId));
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

        public async Task<StockDTO> AddEmptyStockAsync([FromBody] StockToAddDTO stockToAddDTO)
        {
            var userId = _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var existingStock = await _stockRepository.GetAsync(x => (x.Symbol == stockToAddDTO.Symbol && x.User_Id.ToString() == userId));
            if (existingStock == null)
            {
                var transaction = _aspNetCoreNTierDbContext.Database.BeginTransaction();
                stockToAddDTO.User_Id = Int32.Parse(userId);
                stockToAddDTO.Qty = "0";
                stockToAddDTO.Cost_Basis = "0";
                var stock = _mapper.Map<Stock>(stockToAddDTO);
                Log.Information("Adding new empty Stock: {@stockId}", stock);
                var addedStock = await _stockRepository.AddAsync(_mapper.Map<Stock>(stockToAddDTO));
                Log.Information("Added Stock: {@stockId}", addedStock);
                transaction.Commit();
                return _mapper.Map<StockDTO>(addedStock);
            }
            else
            {
               /*Skip adding empty stock if exists */
                Log.Information("Stock exists (skip adding): {@stockId}", existingStock);
                return _mapper.Map<StockDTO>(existingStock);

            }
        }

        public async Task<StockDTO> UpdateOrAddNewStockAsync([FromBody] StockToAddDTO stockToAddDTO)
        {
/*            Log.Information("StockToAdd in UpdateOrAdd Buyer's Stock: {@stockId}", stockToAddDTO);*/
            var userId = stockToAddDTO.User_Id;
            var existingStock = await _stockRepository.GetAsync(x => (x.Symbol == stockToAddDTO.Symbol && x.User_Id == userId));
            if (existingStock == null)
            {
                Log.Information("Create stock for Buyer: {@stockId}", stockToAddDTO);
                var stock = _mapper.Map<Stock>(stockToAddDTO);
                var addedStock = await _stockRepository.AddAsync(_mapper.Map<Stock>(stockToAddDTO));
                Log.Information("Created Stock: {@stockId}", addedStock);
                return _mapper.Map<StockDTO>(addedStock);
            }
            else
            {
                Log.Information("Update existing stock for Buyer: {@stockId}", stockToAddDTO);
                /* Calculate new Cost basis */
                var calcCostBasis= ((Int32.Parse(existingStock.Qty)*float.Parse(existingStock.Cost_Basis)) + (Int32.Parse(stockToAddDTO.Qty)*float.Parse(stockToAddDTO.Cost_Basis))) / (Int32.Parse(existingStock.Qty) + Int32.Parse(stockToAddDTO.Qty));
                existingStock.Cost_Basis = calcCostBasis.ToString();
                existingStock.Qty = (Int32.Parse(existingStock.Qty) + Int32.Parse(stockToAddDTO.Qty)).ToString();
                existingStock.Status = stockToAddDTO.Status;
                var updatedStock = await _stockRepository.UpdateAsync(_mapper.Map<Stock>(existingStock));
                Log.Information("Updated Stock: {@stockId}", updatedStock);
                return _mapper.Map<StockDTO>(updatedStock);
            }
        }

        

        public async Task<StockDTO> UpdateStockAsync(int stockId, StockDTO stockForUpdate)
        {
            var stockToUpdate = await _stockRepository.GetAsync(x => x.Id == stockId);
            if (stockToUpdate == null)
            {
                Log.Information("Stock with Id = {Id} was not found", stockId);
                throw new KeyNotFoundException();
            }

            var updatedStock = _mapper.Map<Stock>(stockForUpdate);

            Log.Information("Updated stock OK:  {@stock} ", updatedStock);

            return _mapper.Map<StockDTO>(await _stockRepository.UpdateAsync(updatedStock));
        }

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

            Log.Information("Updated Stock {@stock}", stockToUpdate);

            return _mapper.Map<StockDTO>(await _stockRepository.UpdateAsync(stock));
        }

        public async Task<StockDTO> BuyStockAsync(string stockId)
        {

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
