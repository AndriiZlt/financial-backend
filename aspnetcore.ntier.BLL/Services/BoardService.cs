using aspnetcore.ntier.BLL.Services.IServices;
using aspnetcore.ntier.DAL.Entities;
using aspnetcore.ntier.DAL.Repositories.IRepositories;
using aspnetcore.ntier.DTO.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Security.Claims;



namespace aspnetcore.ntier.BLL.Services
{
    public class BoardService : IBoardService
    {

        private readonly IBoardRepository _boardRepository;
        private readonly IBoardRepository _stockRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;


        public BoardService (IBoardRepository boardRepository, IBoardRepository stockRepository, IMapper mapper,  IHttpContextAccessor httpContext)
        {
            _boardRepository = boardRepository;
            _stockRepository = stockRepository;
            _mapper = mapper;
            _httpContext = httpContext;
        }

        public async Task<List<BoardItemDTO>> GetBoardAsync(CancellationToken cancellationToken = default)
        {
            var stocksToReturn = await _boardRepository.GetListAsync();
            return _mapper.Map<List<BoardItemDTO>>(stocksToReturn);
        }

        public async Task<BoardItemDTO> AddToBoardAsync([FromBody] BoardItemToAddDTO boardItemToAdd)
        {
            var addedItem = await _boardRepository.AddAsync(_mapper.Map<BoardItem>(boardItemToAdd));
            return _mapper.Map<BoardItemDTO>(addedItem);
        }

        public async Task RemoveFromBoardAsync(int stockId)
        {
            var stockToDelete = await _boardRepository.GetAsync(x => x.Stock_Id == stockId);

            if (stockToDelete is null)
            {
                Log.Information("Stock with Id {stockId} was not found", stockId);
                throw new KeyNotFoundException();
            }

            await _boardRepository.DeleteAsync(stockToDelete);
        }

        /*public async Task<SubtaskDTO> UpdateStatusSubtaskAsync(int taskId)
        {
            var task = await _subtaskRepository.GetAsync(x => x.Id == taskId);
            if (task is null)
            {
                Log.Information("Task with taskId = {TaskId} was not found", taskId);
                throw new KeyNotFoundException();
            }

            string status = task.Status == "undone" ? "completed" : "undone";

            task.Status = status;

            var taskToUpdate = _mapper.Map<Subtask>(task);

            Log.Information("Task with these properties: {@TaskToUpdate} has been updated", task);

            return _mapper.Map<SubtaskDTO>(await _subtaskRepository.UpdateStatusTaskAsync(taskToUpdate));
        }*/

        /*public async Task<SubtaskDTO> UpdateSubtaskAsync(SubtaskDTO taskToUpdate)
        {
            var taskBeforeUpdate = await _subtaskRepository.GetAsync(x => x.Id == taskToUpdate.Id);

            if (taskBeforeUpdate is null)
            {
                Log.Information("Task with taskId = {TaskId} was not found", taskToUpdate.Id);
                throw new KeyNotFoundException();
            }

            taskBeforeUpdate.Title = taskToUpdate.Title;
            taskBeforeUpdate.Description = taskToUpdate.Description;
            taskBeforeUpdate.Status = taskToUpdate.Status;
            taskBeforeUpdate.TaskId = taskToUpdate.TaskId;
            taskBeforeUpdate.DateCompleted = taskToUpdate.DateCompleted;
            taskBeforeUpdate.UserId = (int)taskToUpdate.UserId;

            var taskAfterUpdate = _mapper.Map<Subtask>(taskBeforeUpdate);

            Log.Information("Task with these properties: {@TaskToUpdate} has been updated", taskAfterUpdate);

            return _mapper.Map<SubtaskDTO>(await _subtaskRepository.UpdateTaskAsync(taskAfterUpdate));
        }*/

    }
}

