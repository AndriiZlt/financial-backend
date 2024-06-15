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

        public async Task<StockDTO> AddStockAsync([FromBody] StockToAddDTO stockToAddDTO)
        {
            Log.Information("stockToAddDTO {@stockId}", stockToAddDTO);
/*            var userId = _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);*/
            stockToAddDTO.UserId = Int32.Parse(_httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var stock = _mapper.Map<Stock>(stockToAddDTO);
            var addedStock = await _stockRepository.AddAsync(_mapper.Map<Stock>(stockToAddDTO));

            Log.Information("addedStock {@stockId}", addedStock);
            return _mapper.Map<StockDTO>(addedStock);
        }

 

        public async Task DeleteTaskAsync(int taskId)
        {
            var taskToDelete = await _stockRepository.GetAsync(x => x.Id == taskId);

            if (taskToDelete is null)
            {
                Log.Information("Task with taskId = {TaskId} was not found", taskId);
                throw new KeyNotFoundException();
            }

            await _stockRepository.DeleteAsync(taskToDelete);
        }

        /*public async Task<StockDTO> UpdateStatusTaskAsync(int taskId)
        {
            var task = await _stockRepository.GetAsync(x => x.Stock_Id == taskId.ToString());
            if (task is null)
            {
                Log.Information("Task with taskId = {TaskId} was not found", taskId);
                throw new KeyNotFoundException();
            }

            string status = task.Status == "undone" ? "completed" : "undone";

            task.Status = status;

            var taskToUpdate = _mapper.Map<Taskk>(task);

            Log.Information("Task with these properties: {@TaskToUpdate} has been updated", task);

            return _mapper.Map<StockDTO>(await _stockRepository.UpdateStatusTaskAsync(taskToUpdate));
        }*/

        /*public async Task<TaskDTO> UpdateTaskAsync(TaskDTO taskToUpdate)
        {
            var taskBeforeUpdate = await _stockRepository.GetAsync(x => x.Id == taskToUpdate.Id);

            if (taskBeforeUpdate is null)
            {
                Log.Information("Task with taskId = {TaskId} was not found", taskToUpdate.Id);
                throw new KeyNotFoundException();
            }

*//*            taskBeforeUpdate.Title = taskToUpdate.Title;
            taskBeforeUpdate.Description = taskToUpdate.Description;
            taskBeforeUpdate.Status = taskToUpdate.Status;
            taskBeforeUpdate.UserId= (int)taskToUpdate.UserId;
            taskBeforeUpdate.DateCompleted = taskToUpdate.DateCompleted;*//*

            var taskAfterUpdate = _mapper.Map<Taskk>(taskBeforeUpdate);

            Log.Information("Task with these properties: {@TaskToUpdate} has been updated", taskAfterUpdate);

            return _mapper.Map<TaskDTO>(await _stockRepository.UpdateTaskAsync(taskAfterUpdate));
        }*/


    }
}
