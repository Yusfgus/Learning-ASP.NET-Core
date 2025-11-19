using System;

namespace Design_Principles.Solid.SingleResponsibility.After;

public class AccountService
{
    public void Deposit(Account account, decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be positive");
        
        string transactionMessage;
        
        transactionMessage =
            $"OK Deposit {amount.ToString("C2")}" +
            $", current balance {account.Balance.ToString("C2")}";

        EmailClient.Send(account, transactionMessage, DateTime.Now);
    }

    public void Withdraw(Account account, decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be positive");

        string transactionMessage;
        if (account.Balance < Math.Abs(amount))
        {
            transactionMessage =
            $"OVERDRAFT when trying to withdraw " +
            $"{Math.Abs(amount).ToString("C2")}," +
            $" current balance {account.Balance.ToString("C2")}";
        }
        else
        {
            account.Balance += amount;
            transactionMessage =
            $"OK Withdraw {Math.Abs(amount).ToString("C2")}" +
            $", current balance {account.Balance.ToString("C2")}";
        }

        EmailClient.Send(account, transactionMessage, DateTime.Now);
    }

}
