using System;
using Shared;

namespace Design_Pattern.Singleton.ThreadSafeUsingLock;

class Program
{
    static MemoryLogger logger;

    static void Main(string[] args)
    {
        Utils.printTitle("Design Pattern", width: 80, color: ConsoleColor.Red);
        Utils.printTitle("Singleton ( Thread Safe Using Double CheckedLock )", width: 70, color: ConsoleColor.Blue);

        AssignVoucher("issam@metigator.com", "ABC123");

        UseVoucher("ABC123");

        logger.ShowLog();
    } 
  
    static void AssignVoucher(string email, string voucher)
    {
       logger = MemoryLogger.GetLogger;
         
        // Logic here
        logger.LogInfo($"Voucher '{voucher}' assigned"); 

        // another logic
        logger.LogError($"unable to send email '{email}'"); 
    }

    static void UseVoucher(string voucher)
    {
        logger = MemoryLogger.GetLogger;

        // Logic here
        logger.LogWarning($"3 attempts made to validate the voucher");

        // Logic here
        logger.LogInfo($"'{voucher}' is used"); 
    }
}