using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    internal class Customer
    {
        public static int _counter = 0;
        public int Id { get; }
        public string Name { get; set; }
        public string NtionId { get;private set; }
        public DateTime BirthoFDate { get;private set; }
        public List<Account> Accounts { get; set; }
        public Customer(string name, string ntionId, DateTime birthoFDate)
        {
            Id = ++_counter;
            Name = name;
            NtionId = ntionId;
            BirthoFDate = birthoFDate;
            Accounts = new List<Account>();
        }
        public void TotalBlaance()
        {
            var total = Accounts.Sum(a => a.Balance);
            Console.WriteLine($"Total balance for customer {Name}: {total}");
        }
        private decimal TotalBalance(List<Account> accounts)
        {
            decimal total = 0;
            foreach (var account in accounts)
            {
                total += account.Balance;
            }
            return total;
        }
    }
}
