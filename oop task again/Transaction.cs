using System;
using System.Collections.Generic;
using System.Text;

namespace oop_task_again
{
    internal class Transaction
    {
        private static int _counter = 0;
        public int TransactionId { get; }
        public DateTime Date { get; }
        public TransactionType Type { get; }
        public decimal Amount { get;}
        public int? TargetAccountNumber { get; } 

        public Transaction(TransactionType type, decimal amount, int? target = null)
        {
            TransactionId=++_counter;
            Date = DateTime.Now;
            Type = type;
            Amount = amount;
            TargetAccountNumber = target;
        }
    }
    public enum TransactionType
    {
        Deposit,
        Withdraw,
        Transfer
    }
}

