using System;
using System.ComponentModel;
using System.Linq;
using EF_Core.Transactions.Data;
using EF_Core.Transactions.Entities;
using EF_Core.Transactions.Helpers;
using Shared;

public class Program
{
    private static Random _random = new Random();

    public static void Main()
    {
        Utils.printTitle(title: "EF_Core ( Transaction )", color: ConsoleColor.Red, width: 80);

        // DatabaseHelper.RecreateCleanDatabase();

        string choice;
        bool flag = true;
        while (flag)
        {
            Console.Write("""
            Enter a choice:
            ───────────────
            0) Initialize Database.
            1) Show accounts.
            2) Multiple SaveChanges.
            3) Single SaveChanges.
            4) Multiple SaveChanges with Transaction.
            5) Multiple SaveChanges with Transaction Best Practice.
            6) Multiple SaveChanges with Transaction Save Points.
            Any other key to exit.
            ───> 
            """);

            choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "0":
                        DatabaseHelper.InitializeDatabase();
                        break;
                    case "1":
                        DatabaseHelper.ShowAccounts();
                        break;
                    case "2":
                        Multiple_SaveChanges();
                        break;
                    case "3":
                        Single_SaveChanges();
                        break;
                    case "4":
                        Multiple_SaveChanges_WithTransaction();
                        break;
                    case "5":
                        Multiple_SaveChanges_WithTransaction_BestPractice();
                        break;
                    case "6":
                        Multiple_SaveChanges_WithTransaction_SavePoints();
                        break;
                    default:
                        flag = false;
                        break;
                }
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ResetColor();
            }

            Console.WriteLine("──────────────────────────────────────────────────────────────────\n");
        }
    }

    private static void Multiple_SaveChanges()
    {
        Console.Write("Enter first account id: ");
        string account1Id = Console.ReadLine();
        Console.Write("Enter second account id: ");
        string account2Id = Console.ReadLine();
        Console.Write("Enter amount to transfer: ");
        decimal amount = Convert.ToDecimal(Console.ReadLine());

        using (var context = new AppDbContext())
        {
            BankAccount account1 = context.BankAccounts.Single(c => c.AccountId == account1Id);
            BankAccount account2 = context.BankAccounts.Single(c => c.AccountId == account2Id);

            account1.WithDraw(amount);
            context.SaveChanges();

            if (_random.Next(0, 2) == 0) // 50%
                throw new Exception("Something went wrong");

            account2.Deposit(amount);
            context.SaveChanges();

            Console.WriteLine("Transfer completed successfully");
        }
    }

    private static void Single_SaveChanges()
    {
        Console.Write("Enter first account id: ");
        string account1Id = Console.ReadLine();
        Console.Write("Enter second account id: ");
        string account2Id = Console.ReadLine();
        Console.Write("Enter amount to transfer: ");
        decimal amount = Convert.ToDecimal(Console.ReadLine());

        using (var context = new AppDbContext())
        {
            BankAccount account1 = context.BankAccounts.Single(c => c.AccountId == account1Id);
            BankAccount account2 = context.BankAccounts.Single(c => c.AccountId == account2Id);

            account1.WithDraw(amount);

            if (_random.Next(0, 2) == 0) // 50%
                throw new Exception("Something went wrong");

            account2.Deposit(amount);

            context.SaveChanges();

            Console.WriteLine("Transfer completed successfully");
        }
    }

    private static void Multiple_SaveChanges_WithTransaction()
    {
        Console.Write("Enter first account id: ");
        string account1Id = Console.ReadLine();
        Console.Write("Enter second account id: ");
        string account2Id = Console.ReadLine();
        Console.Write("Enter amount to transfer: ");
        decimal amount = Convert.ToDecimal(Console.ReadLine());

        using (var context = new AppDbContext())
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                BankAccount account1 = context.BankAccounts.Single(c => c.AccountId == account1Id);
                BankAccount account2 = context.BankAccounts.Single(c => c.AccountId == account2Id);

                account1.WithDraw(amount);
                context.SaveChanges();

                if (_random.Next(0, 2) == 0) // 50%
                    throw new Exception("Something went wrong");

                account2.Deposit(amount);
                context.SaveChanges();

                transaction.Commit();

                Console.WriteLine("Transfer transaction completed successfully");
            }
        }
    }

    private static void Multiple_SaveChanges_WithTransaction_BestPractice()
    {
        Console.Write("Enter first account id: ");
        string account1Id = Console.ReadLine();
        Console.Write("Enter second account id: ");
        string account2Id = Console.ReadLine();
        Console.Write("Enter amount to transfer: ");
        decimal amount = Convert.ToDecimal(Console.ReadLine());

        using (var context = new AppDbContext())
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    BankAccount account1 = context.BankAccounts.Single(c => c.AccountId == account1Id);
                    BankAccount account2 = context.BankAccounts.Single(c => c.AccountId == account2Id);

                    account1.WithDraw(amount);
                    context.SaveChanges();

                    if (_random.Next(0, 2) == 0) // 50%
                        throw new Exception("Something went wrong");

                    account2.Deposit(amount);
                    context.SaveChanges();

                    transaction.Commit();

                    Console.WriteLine("Transfer transaction completed successfully");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    transaction.Rollback();
                }
            }
        }
    }
    
    private static void Multiple_SaveChanges_WithTransaction_SavePoints()
    {
        Console.Write("Enter first account id: ");
        string account1Id = Console.ReadLine();
        Console.Write("Enter second account id: ");
        string account2Id = Console.ReadLine();
        Console.Write("Enter amount to transfer: ");
        decimal amount = Convert.ToDecimal(Console.ReadLine());

        using (var context = new AppDbContext())
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    BankAccount account1 = context.BankAccounts.Single(c => c.AccountId == account1Id);
                    BankAccount account2 = context.BankAccounts.Single(c => c.AccountId == account2Id);

                    transaction.CreateSavepoint("Read_Accounts");

                    account1.WithDraw(amount);
                    context.SaveChanges();

                    transaction.CreateSavepoint("Withdraw_done");

                    if (_random.Next(0, 2) == 0) // 50%
                        throw new Exception("Something went wrong");

                    account2.Deposit(amount);
                    context.SaveChanges();

                    transaction.CreateSavepoint("Deposit_done");

                    transaction.Commit();

                    Console.WriteLine("Transfer transaction completed successfully");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    transaction.RollbackToSavepoint("Withdraw_done");
                }
            }
        }
    }
    
}