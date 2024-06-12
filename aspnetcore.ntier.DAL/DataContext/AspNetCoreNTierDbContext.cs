using aspnetcore.ntier.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace aspnetcore.ntier.DAL.DataContext;

public class AspNetCoreNTierDbContext :IdentityDbContext<IdentityUser>
{
    public AspNetCoreNTierDbContext(DbContextOptions<AspNetCoreNTierDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }

    public DbSet<Taskk> Tasks { get; set; }
    public DbSet<Subtask> Subtasks { get; set; }
    public DbSet<Friend> Friends { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder.Entity<User>().HasData(
             new User
             {
                 Id = 1,
                 UserName = "user0",
                 Email = "johndoe@gmail.com",
                 Password = "Zxcv@345",
                 Name = "John",
                 Surname = "Doe",
             }
         );


        modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole() { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
            new IdentityRole() { Name = "User", ConcurrencyStamp = "2", NormalizedName = "User" }
            );

        modelBuilder.Entity<Subtask>()
            .HasOne(s => s.Task)
            .WithMany(t => t.Subtasks)
            .HasForeignKey(t => t.TaskId)
            .IsRequired();

        modelBuilder.Entity<Taskk>()
            .HasOne(t => t.User)
            .WithMany(u => u.Tasks)
            .HasForeignKey(t => t.UserId)
            .IsRequired();

        modelBuilder.Entity<Subtask>()
            .HasOne(t => t.User)
            .WithMany(u => u.Subtasks)
            .HasForeignKey(t => t.UserId)
            .IsRequired();
    }
}