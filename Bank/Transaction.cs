using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    internal class Transaction
    {
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }
        public DateTime Date { get; set; }
        public int? TargetAccountNumber { get; set; } 
    }
}
