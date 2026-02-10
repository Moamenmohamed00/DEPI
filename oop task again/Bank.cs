using System;
using System.Collections.Generic;
using System.Text;

namespace oop_task_again
{
    internal class Bank
    {
        private string _branchCode;
        public string BankName { get; set; }
        public List<Customer>Customers;// cutomer m-1 bank
        public List<BankAccount> BankAccounts { get; set; }
        public string BranchCode {
            get=>_branchCode; 
            set {
                if (value.StartsWith("BR"))
                { _branchCode = value; } } }
        public Bank():this("unknown","BR0000")
        {
        }
        public Bank(string name,string branch)
        {
            BankName = name;
            BranchCode = branch;
            BankAccounts = new List<BankAccount>();
            Customers = new List<Customer>();
        }
        public Customer SearchCustomerByName(string name)
        {
            return Customers.FirstOrDefault(c =>
                c.FullName.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public Customer SearchCustomerById(string nationalId)
        {
            return Customers.FirstOrDefault(c => c.NationalID == nationalId);
        }

        public void RemoveCustomer(Customer customer)
        {
            if (customer == null)
            {
                Console.WriteLine("Customer not found");
                return;
            }

            bool allZero = customer.Accounts.All(acc => acc.Balance == 0);

            if (allZero)
            {
                Customers.Remove(customer);
                Console.WriteLine("Customer removed successfully");
            }
            else
            {
                Console.WriteLine("Cannot remove customer. Accounts still have balance.");
            }
        }
        public void ShowBankReport()
        {
            Console.WriteLine($"Bank: {BankName}, Branch: {BranchCode}");
            Console.WriteLine($"Total Customers: {Customers.Count}");
            Console.WriteLine($"Total Accounts: {Customers.Sum(c => c.Accounts.Count)}");
            Console.WriteLine($"Total Balance: {Customers.Sum(c => c.Accounts.Sum(a => a.Balance)):C}");
        }
    }
}
