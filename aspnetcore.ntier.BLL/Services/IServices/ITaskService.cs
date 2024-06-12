
using aspnetcore.ntier.DAL.Entities;
using aspnetcore.ntier.DTO.DTOs;

namespace aspnetcore.ntier.BLL.Services.IServices;

public interface ITaskService
{
    Task<List<TaskDTO>> GetTasksAsync(CancellationToken cancellationToken = default);

    Task<TaskDTO> AddTaskAsync(TaskToAddDTO taskToAddDTO);

    Task DeleteTaskAsync(int taskId);

    Task<TaskDTO> UpdateStatusTaskAsync(int taskId);

    Task<TaskDTO> UpdateTaskAsync(TaskDTO taskToUpdate);
}
