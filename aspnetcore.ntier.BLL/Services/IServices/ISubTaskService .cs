
using aspnetcore.ntier.DAL.Entities;
using aspnetcore.ntier.DTO.DTOs;

namespace aspnetcore.ntier.BLL.Services.IServices;

public interface ISubtaskService
{
    Task<List<SubtaskDTO>> GetSubtasksAsync(CancellationToken cancellationToken = default);

    Task<SubtaskDTO> AddSubtaskAsync(SubtaskToAddDTO taskToAddDTO);

    Task DeleteSubtaskAsync(int taskId);

    Task<SubtaskDTO> UpdateStatusSubtaskAsync(int taskId);

    Task<SubtaskDTO> UpdateSubtaskAsync(SubtaskDTO taskToUpdate);
}
