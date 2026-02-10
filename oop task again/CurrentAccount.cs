using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace oop_task_again
{
    internal class CurrentAccount:BankAccount
    {
        public decimal OverdraftLimit {  get; set; }
        public CurrentAccount(string fullname, string nationalid, string phonenumber, string address, decimal balance, decimal overdraftlimit, string Bankname, string Branchcode) :base(fullname, nationalid, phonenumber, address, balance,Bankname,Branchcode)
        {
            OverdraftLimit=overdraftlimit;
        }

        public override void ShowAccountDetails()
        {
            base.ShowAccountDetails();
            Console.WriteLine($"OverdraftLimit:{OverdraftLimit}");
        }
        public override void CalculateInterest()//show
        {
            base.CalculateInterest();
            Balance -= OverdraftLimit;
            Console.WriteLine($"after:{Balance}");
        }
        public override string AccountDetails()
        {
            return base.AccountDetails();
        }
        public override bool Withdraw(decimal amount)
    {
        if (amount <= 0)
            return false;

        if (Balance + OverdraftLimit < amount)
        {
            Console.WriteLine("Overdraft limit exceeded");
            return false;
        }

        // السحب مسموح
        // ممكن يصبح رصيد سلبي
        typeof(BankAccount)
            .GetProperty("Balance")
            .SetValue(this, Balance - amount);

        Transactions.Add(new Transaction(TransactionType.Withdraw, amount, null));

        return true;
    }
    }
}
