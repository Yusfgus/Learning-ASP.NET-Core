using System;

namespace Design_Principles.Solid.SingleResponsibility.After;

public class Account
{
    public string Name { get; set; }
    public string Email { get; set; }
    public decimal Balance { get; set; }

    public Account(string name, string email, decimal balance)
    {
        this.Name = name;
        this.Email = email;
        this.Balance = balance;
    }
}

