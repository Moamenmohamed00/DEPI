using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Text;

namespace oop_task_again
{
    internal class Customer
    {
        private string _customerID;
        private string _fullname;
        private string _nationalID;
        private DateTime _birthofdate;
        private static int _customerCount = 0;
        public string CustomerID { get; }
        public string FullName { get; set; }
        public string NationalID { get; private set; }
        public DateTime BirthOfDate { get; set; }
        public List<BankAccount> Accounts { get; private set; }
        public Customer()
        {
            CustomerID = GenrateCustId();
            NationalID = "00000000000000";
            FullName = "unknow";
            BirthOfDate =  DateTime.MinValue;
            Accounts = new List<BankAccount>();
        }
        private string GenrateCustId()
        {
            ++_customerCount;
            return $"Cust{_customerCount:D5}";
        }
        //public string UpdateCust(Customer cust,string name,DateTime dateTime)
        //{
        //    cust.FullName = name;
        //    cust.BirthOfDate = dateTime;
        //    return $"customerID:{cust.CustomerID} is updated,name:{cust.FullName},birthDate:{cust.BirthOfDate}";
        //}
        //public Customer UpdateCustname(Customer cust, string name)
        //{
        //    cust.FullName = name;
        //    return cust;
        //}
        public void UpdateName(string name)
        {
            if (!string.IsNullOrEmpty(name))
                FullName = name;
            else
                Console.WriteLine("invalid name");
        }
        public void UpdateBirth(DateTime birth)
        {
            BirthOfDate=birth;
        }
        public void AddAccount(BankAccount account)
        {
            Accounts.Add(account);
        }
        //public void RemoveAccount(BankAccount account)
        //{
        //    if (account.Balance == 0 && Accounts.Contains(account))
        //    {
        //        Accounts.Remove(account);
        //    }
        //    else
        //        Console.WriteLine($"withdraw yor money:{account.Balance}");
        //}

    }
}
