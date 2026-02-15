using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    internal sealed class Saving:Account,IReportable
    {
        public decimal InterestRate { get; set; }

        public override string AccountType() => "Saving";

        public override decimal CalculateMonthly()=>Balance* InterestRate / 100/12;
        public override bool Withdraw(decimal amount)
        {
            if (Balance >= amount)
            {
                Balance -= amount;
                return true;
            }
            else
            {
                Console.WriteLine("Insufficient funds.");
            }
            return false;
        }

        string IReportable.GetReport()
        {
            return $"Saving Account report" +
               $"\nAccount Number: {AccountNumber}" +
               $"\nBalance: {InterestRate:C}" +
               $"\nOverdraft Limit: {InterestRate:C}" +
               $"\nDate Opened: {DateOpened:d}" +
               $"\nTransactions: {Transactions.Count}"+
               $"\n Monthly rate: {CalculateMonthly()}%";

        }
    }
}
