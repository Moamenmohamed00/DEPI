using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
    internal class Transaction
    {
        public DateTime Date { get; set; }
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
        public int? TargetAccountNumber { get; set; } // للـ Transfer فقط

        public Transaction(TransactionType type, decimal amount, int? target = null)
        {
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
