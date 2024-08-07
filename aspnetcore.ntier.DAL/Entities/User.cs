﻿using Microsoft.AspNetCore.Identity;

namespace aspnetcore.ntier.DAL.Entities;

public class User : IdentityUser<int>
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Ballance { get; set; }
    public ICollection<Stock> Stocks { get; set; }
    public ICollection<BoardItem> BoardItems { get; set; }
    public ICollection<Transaction> Transactions { get; set; }
    public ICollection<Notification> Notifications { get; set; }
    public ICollection<AlpacaTransaction> AlpacaTransactions { get; set; }

}         