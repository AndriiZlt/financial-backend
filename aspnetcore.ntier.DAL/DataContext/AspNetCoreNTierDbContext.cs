﻿using aspnetcore.ntier.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace aspnetcore.ntier.DAL.DataContext;

public class AspNetCoreNTierDbContext :IdentityDbContext<IdentityUser>
{
    public AspNetCoreNTierDbContext(DbContextOptions<AspNetCoreNTierDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Stock> Stocks { get; set; }
    public DbSet<BoardItem> BoardItems { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<AlpacaTransaction> AlpacaTransactions { get; set; }

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
                 Ballance="2000",
             },
             new User
             {
                Id = 2,
                UserName = "user2",
                Email = "johndoe@gmail.com",
                Password = "zxc",
                Name = "Mykola",
                Surname = "Doe",
                Ballance = "2000",
             },
             new User
             {
                 Id = 3,
                 UserName = "alpaca",
                 Email = "alpaca@gmail.com",
                 Password = "zxc",
                 Name = "Alpaca",
                 Surname = "API",
                 Ballance = "1000000",
             }
         );

        modelBuilder.Entity<AlpacaTransaction>()
            .HasOne(s => s.User)
            .WithMany(t => t.AlpacaTransactions)
            .HasForeignKey(t => t.User_Id)
            .IsRequired();

        modelBuilder.Entity<Transaction>()
            .HasMany(t => t.Users)
            .WithMany(u => u.Transactions)
            .UsingEntity(j => j.ToTable("UserTransaction"));

        modelBuilder.Entity<Stock>()
            .HasOne(s => s.BoardItem)
            .WithOne(t => t.Stock)
            .HasForeignKey<BoardItem>(t => t.Stock_Id)
            .IsRequired();

        modelBuilder.Entity<Stock>()
            .HasOne(s => s.User)
            .WithMany(t => t.Stocks)
            .HasForeignKey(t => t.User_Id)
            .IsRequired();

        modelBuilder.Entity<BoardItem>()
            .HasOne(s => s.User)
            .WithMany(t => t.BoardItems)
            .HasForeignKey(t => t.User_Id)
            .IsRequired();

        modelBuilder.Entity<Notification>()
            .HasOne(s => s.User)
            .WithMany(t => t.Notifications)
            .HasForeignKey(t => t.User_Id)
            .IsRequired();
    }
}