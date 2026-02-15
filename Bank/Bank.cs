using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    internal class Bank
    {
        public string Name { get; set; }
        public string BranchCode { get; set; }
        public Data<Account> Accounts { get; }
        public Data<Transaction> Transactions { get; }
        public Data<Customer> Customers { get; }
        public Bank(string name, string branchCode)
        {
            Name = name;
            BranchCode = branchCode;
            Accounts = new Data<Account>();
            Transactions = new Data<Transaction>();
            Customers = new Data<Customer>();
        }
        public void AddCustomer(string name, string nid,DateTime date)
        {
            Customers.Add(new Customer(name,nid,date));
        }
         public void AddSavingAccount(Customer customer,decimal interrate)
        {
            Accounts.Add(new Saving(interrate));
           customer.Accounts.Add(Accounts.GetAll().Last());
        }
         public void AddTransaction(Transaction transaction)
        {
            Transactions.Add(transaction);
        }
    }
}
