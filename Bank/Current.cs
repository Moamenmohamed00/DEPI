using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    internal sealed class Current:Account,IReportable
    {
        public decimal OverDraft { get; set; }

        public override string AccountType()
        {
        return "current";
        }

        public override decimal CalculateMonthly() => 0;
        public override bool Withdraw(decimal amount)
        {
            if (Balance + OverDraft >= amount)
            {
                Balance -= amount;
                return true;
            }
            else
            {
                Console.WriteLine("Insufficient funds, including overdraft.");
            }
            return false;
        }

        public string GetReport()
        {
            return $"Current Account report"+
                $"\nAccount Number: {AccountNumber}"+
                $"\nBalance: {Balance:C}"+
                $"\nOverdraft Limit: {OverDraft:C}"+
                $"\nDate Opened: {DateOpened:d}"+
                $"\nTransactions: {Transactions.Count}";
        }
    }
}
