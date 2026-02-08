using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
    internal class SavingAccount:BankAccount
    {
        private decimal InterestRate;
       public SavingAccount(string fullname, string nationalid, string phonenumber, string address, decimal InterestRate, decimal balance , string bankname, string branchcode) : base(fullname, nationalid, phonenumber, address, balance, bankname, branchcode)
        {
            this.InterestRate = InterestRate;
            Balance= CalculateInterest();
        }

        public override decimal CalculateInterest()
        {
            return Balance*( 1 + InterestRate);
        }

        public override void ShowAccountDetails()//print this method
        {
            Console.WriteLine($"full name: {FullName},national id: {NationalID},phone number: {PhoneNumber},address: {Address},balance :{Balance},InterestRate{InterestRate},dateopened{CreatedDate},AccountNumber{AccountNumber}");
        }
        public override bool Withdraw(decimal amount)
        {
            if (amount > 0)
            {
                if (Balance - amount >= 0)
                {
                    Balance -= amount;
                    Console.WriteLine($"Withdrew {amount:C}. New balance: {Balance:C}");
                    return true;
                }
                else
                {
                    Console.WriteLine("Insufficient funds for withdrawal.");
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
