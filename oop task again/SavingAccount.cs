using System;
using System.Collections.Generic;
using System.Text;

namespace oop_task_again
{
    internal class SavingAccount:BankAccount
    {
        public decimal InterestRate {  get; set; }
       public SavingAccount(string fullname, string nationalid, string phonenumber, string address,decimal balance,decimal interestrate,string bankname,string branchcode) :base(fullname,nationalid,phonenumber,address,balance,bankname,branchcode)
        {
            InterestRate = interestrate;
        }
        public SavingAccount(string fullname, string nationalid, string phonenumber, string address, decimal interestrate, string Bankname, string Branchcode) : base(fullname, nationalid, phonenumber, address,Bankname,Branchcode)
        {
            InterestRate = interestrate;
        }
        public override void ShowAccountDetails()
        {
            base.ShowAccountDetails();
            Console.WriteLine($"InterestRate:{InterestRate}");
        }
        public override void CalculateInterest()//change balance
        {
            base.CalculateInterest();
            Balance *= InterestRate;
            Console.WriteLine($"after:{Balance}");
        }
        public override string AccountDetails()
        {
            return base.AccountDetails();
        }
    }
}
