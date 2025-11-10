using System;

namespace EF_Core.Transactions.Entities;

public class GLTransaction
{
    public int Id { get; }
    public decimal Amount { get; }
    public string Notes { get; }
    public DateTime CreatedAt { get; }

    public GLTransaction(decimal amount, string notes, DateTime createdAt)
    {
        Amount = amount;
        Notes = notes;
        CreatedAt = createdAt;
    }
}