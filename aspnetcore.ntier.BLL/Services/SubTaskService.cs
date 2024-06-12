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
    public class SubtaskService : ISubtaskService
    {

        private readonly ISubtaskRepository _subtaskRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;


        public SubtaskService (ISubtaskRepository subtaskRepository, IMapper mapper,  IHttpContextAccessor httpContext)
        {
            _subtaskRepository = subtaskRepository;
            _mapper = mapper;
            _httpContext = httpContext;
        }

        public async Task<List<SubtaskDTO>> GetSubtasksAsync(CancellationToken cancellationToken = default)
        {
            var userId = _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var subtasksToReturn = await _subtaskRepository.GetListAsync(Int32.Parse(userId));
            return _mapper.Map<List<SubtaskDTO>>(subtasksToReturn);
        }

        public async Task<SubtaskDTO> AddSubtaskAsync([FromBody] SubtaskToAddDTO subtaskToAddDTO)
        {
            var userId = _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            subtaskToAddDTO.UserId = Convert.ToInt32(userId);

            var addedSubtask = await _subtaskRepository.AddAsync(_mapper.Map<Subtask>(subtaskToAddDTO));
            return _mapper.Map<SubtaskDTO>(addedSubtask);
        }

        public async Task DeleteSubtaskAsync(int taskId)
        {
            var taskToDelete = await _subtaskRepository.GetAsync(x => x.Id == taskId);

            if (taskToDelete is null)
            {
                Log.Information("Task with taskId = {TaskId} was not found", taskId);
                throw new KeyNotFoundException();
            }

            await _subtaskRepository.DeleteAsync(taskToDelete);
        }

        public async Task<SubtaskDTO> UpdateStatusSubtaskAsync(int taskId)
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
        }

        public async Task<SubtaskDTO> UpdateSubtaskAsync(SubtaskDTO taskToUpdate)
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
        }

    }
}

