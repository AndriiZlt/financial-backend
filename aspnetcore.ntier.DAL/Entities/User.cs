using Microsoft.AspNetCore.Identity;

namespace aspnetcore.ntier.DAL.Entities;

public class User : IdentityUser<int>
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public ICollection<Taskk> Tasks { get; set; }
    public ICollection<Subtask> Subtasks { get; set; }

}         