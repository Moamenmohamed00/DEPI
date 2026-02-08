using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
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
        public Customer( string fullname, string nationalid, DateTime birthofdate)
            {
                FullName = fullname;
                NationalID = nationalid;
                BirthOfDate = birthofdate;
                CustomerID = GenerateCustomerID();
                Accounts = new List<BankAccount>();
        }
        private string GenerateCustomerID()
        {
            _customerCount++;
            return $"CUST{_customerCount:D5}";
        }
    }
}
