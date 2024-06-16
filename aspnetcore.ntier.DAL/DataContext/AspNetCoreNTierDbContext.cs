using aspnetcore.ntier.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace aspnetcore.ntier.DAL.DataContext;

public class AspNetCoreNTierDbContext :IdentityDbContext<IdentityUser>
{
    public AspNetCoreNTierDbContext(DbContextOptions<AspNetCoreNTierDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }

    public DbSet<Stock> Stocks { get; set; }
    public DbSet<BoardItem> BoardStocks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder.Entity<User>().HasData(
             new User
             {
                 Id = 1,
                 UserName = "user1",
                 Email = "johndoe@gmail.com",
                 Password = "zxc",
                 Name = "Andrii",
                 Surname = "Doe",
             },
             new User
             {
                Id = 2,
                UserName = "user2",
                Email = "johndoe@gmail.com",
                Password = "zxc",
                Name = "Mykola",
                Surname = "Doe",
             }
         );

        modelBuilder.Entity<Stock>()
            .HasOne(s => s.BoardStock)
            .WithOne(t => t.Stock)
            .HasForeignKey<BoardItem>(t=>t.Stock_Id)
            .IsRequired();

        modelBuilder.Entity<Stock>()
            .HasOne(s => s.User)
            .WithMany(t => t.Stocks)
            .HasForeignKey(t => t.UserId)
            .IsRequired();

        modelBuilder.Entity<BoardItem>()
            .HasOne(s => s.User)
            .WithMany(t => t.BoardItems)
            .HasForeignKey(t => t.User_Id)
            .IsRequired();


    }
}