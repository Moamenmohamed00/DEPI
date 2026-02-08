using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
    internal class CurrentAccount:BankAccount
    {
      private decimal OverdraftLimit;
       public CurrentAccount( string fullname,string nationalid,string phonenumber,string address, decimal overdraft, decimal balance ,string bankname, string branchcode) :base(fullname, nationalid, phonenumber, address, balance, bankname, branchcode)
        {
            OverdraftLimit = overdraft;
            Balance= CalculateInterest();
        }

  

        public override decimal CalculateInterest()
        {
            return Balance-OverdraftLimit;
        }

        public new void ShowAccountDetails()// print ShowAccountDetails in bankaccount class not this method 
        {
            Console.WriteLine($"full name: {FullName},national id: {NationalID},phone number: {PhoneNumber},address: {Address},balance :{Balance},OverdraftLimit{OverdraftLimit}");
        }
        public override bool Withdraw(decimal amount)
        {
            if (amount > 0)
            {
                if (Balance - amount >= -OverdraftLimit)
                {
                    Balance -= amount;
                    Console.WriteLine($"Withdrew {amount:C}. New balance: {Balance:C}");
                    return true;
                }
                else
                {
                    Console.WriteLine("Withdrawal would exceed overdraft limit.");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Withdrawal amount must be positive.");
                return false;
            }
        }
    }
}
