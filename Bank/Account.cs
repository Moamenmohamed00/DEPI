using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    public delegate void LogHandler(string message, string account);
    internal abstract class Account
    {
        public static int _counter = 0;
        public int AccountNumber { get; }
        public decimal Balance { get;protected set; }
        public DateTime DateOpened { get;  }
        public List<Transaction> Transactions { get; set; }
        LogHandler logHandler;
        public Account(int accountNumber, decimal balance, DateTime dateOpened, List<Transaction> transactions, LogHandler logHandler)
        {
            AccountNumber = accountNumber;
            Balance = balance;
            DateOpened = dateOpened;
            Transactions = transactions;
            this.logHandler = logHandler;
        }
        public Account(int accountNumber,decimal balance)
        {            
            AccountNumber = accountNumber;
            Balance = balance;
            DateOpened = DateTime.Now;
            Transactions = new List<Transaction>();
        }

        protected Account() 
        {
            AccountNumber=++_counter;
            DateOpened=DateTime.Now;
            Balance =0;
            Transactions = new List<Transaction>();
        }
        public void Deposit(decimal amount)
        {
            if (amount > 0)
            {
                Balance += amount;
                AddTransaction(TransactionType.deposit, amount, null);
                Action<string,string> log = (message,acouount) => Console.WriteLine($"Log: {message},acount{acouount}");
                log("Deposit made", AccountNumber.ToString());
            }
            else
            {
                Console.WriteLine("not allow");
            }
            
        }
        public virtual bool Withdraw(decimal amount)
        {
            if (Balance >= amount)
            {
                Balance -= amount;
                AddTransaction(TransactionType.withdraw, amount, null);
                logHandler?.Invoke("Withdrawal made", AccountNumber.ToString());
                return true;
            }
            else
            {
                Console.WriteLine("Insufficient funds.");
            }
            return false;
        }
        public abstract decimal CalculateMonthly();
        public abstract string AccountType();
        public void AddTransaction(TransactionType transaction,decimal amount,int? account_number)
        {
            Transactions.Add(new Transaction
            {
                Amount = amount,
                Type = transaction,
                Date = DateTime.Now,
                TargetAccountNumber=account_number
            });
        }
    }
}
