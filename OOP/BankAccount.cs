using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
    internal class BankAccount
    {
        const string BankCode = "BNK001";
        protected DateTime CreatedDate;
        private int _accountNumber;
        private string _fullname;
        private string _nationalID;
        private string _phoneNumber;
        private string _address;
        private decimal _balance;
        private static int counter = 0;
        public List<Customer> customers { get; private set; }
        public string bankName { get; private set; }
        public string branchCode { get; private set; }
        public int AccountNumber => _accountNumber;
        public List<Transaction>? Transactions { get; private set; }
        public string FullName
        {
            get { return _fullname; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                { _fullname = value; }
                else
                {
                    Console.WriteLine("full name please");
                }
            }
        }
        public string NationalID
        {
            get => _nationalID;
            private set
            {
                if (IsValidNationalID(value))
                {
                    _nationalID = value;
                }
                else
                {
                    Console.WriteLine("full national id (14 digits)");
                }
            }
        }
        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                if (IsValidPhoneNumber(value))
                { _phoneNumber = value; }
                else { Console.WriteLine("11 digits start with \"01\""); }
            }
        }
        public decimal Balance
        {
            get => _balance;
            set
            {
                if (value >= 0)
                { _balance = value; }
            }
        }
        public string? Address { get; set; }

        public BankAccount()
        {
            CreatedDate = DateTime.Now;
            _accountNumber = 0;
            FullName = "noun";
            NationalID = "000000000000000";
            PhoneNumber = "noun";
            Address = "noun";
            Balance = 0;
        }
        public BankAccount(string _fullname, string _nationalID, string _phonenumber, string _address, decimal balance, string bankname, string branchcode)
        {
            CreatedDate = DateTime.Now;
            FullName = _fullname;
            NationalID = _nationalID;
            PhoneNumber = _phonenumber;
            Address = _address;
            Balance = balance;
            _accountNumber = GenrateAcountNumber();
            bankName = bankname;
            branchCode = branchcode;
                customers = new List<Customer>();
            Transactions= new List<Transaction>();
        }
        public BankAccount(string _fullname, string _nationalID, string _phonenumber, string _address, string bankname, string branchcode) : this(_fullname, _nationalID, _phonenumber, _address, 0, bankname, branchcode)
        {

        }
        public BankAccount(string bankname, string branchcode)
        {
            CreatedDate = DateTime.Now;
            _accountNumber = GenrateAcountNumber();
            bankName = bankname;
            branchCode = branchcode;
            customers = new List<Customer>();
        }
        private static int GenrateAcountNumber()
        {
            return ++counter;
        }
        public virtual void ShowAccountDetails()
        {
            Console.WriteLine($"full name: {FullName},national id: {NationalID},phone number: {PhoneNumber},address: {Address},balance :{Balance},account number:{_accountNumber},created at: {CreatedDate}");
        }
        private bool IsValidNationalID(string nationalID)
        {
            return nationalID.Length == 14;
        }
        private bool IsValidPhoneNumber(string phonenumber)
        {
            return phonenumber.Length == 11 && phonenumber.StartsWith("01");
        }
        public virtual decimal CalculateInterest()
        {
            return Balance;
        }
        public Customer AddCustomer(string name, string nid, DateTime dob)
        {
            Customer customer = new Customer(name, nid, dob);
            customers.Add(customer);
            return customer;
        }
        public string RemoveCustomer(string customerID)
        {
            Customer customerToRemove = customers.FirstOrDefault(c => c.CustomerID == customerID);
            if (customerToRemove.Accounts.Any(acc => acc.Balance > 0))
            {
                return "can't remove this customer because he has accounts with balance more than 0";
            }
            else
            {
                customers.Remove(customerToRemove);
                return $"Customer with ID {customerID} not found.";
            }
        }
        public Customer updateCustomer(string id, string name, DateTime dob)
        {
            Customer customerToUpdate = customers.FirstOrDefault(c => c.CustomerID == id);
            if (customerToUpdate != null)
            {
                customerToUpdate.FullName = name;
                customerToUpdate.BirthOfDate = dob;
                return customerToUpdate;
            }
            else
            {
                Console.WriteLine($"Customer with ID {id} not found.");
                return null;
            }
        }
        public List<Customer> Serachbyname(string name)
        {
            return customers.Where(c => c.FullName.Contains(name, StringComparison.OrdinalIgnoreCase))
            .ToList();
        }
        public List<Customer> Serachbyid(string nid)
        {
            return customers.Where(c => c.NationalID.Contains(nid, StringComparison.OrdinalIgnoreCase))
            .ToList();
        }
        public void Deposit(decimal amount)
        {
            if (amount > 0)
            {
                Balance += amount;
                AddTransaction(TransactionType.Deposit, amount);
                Console.WriteLine($"Deposited {amount:C}. New balance: {Balance:C}");
            }
            else
            {
                Console.WriteLine("Deposit amount must be positive.");
            }
        }
        public virtual bool Withdraw(decimal amount)
        {
            if (amount > 0 && amount <= Balance)
            {
                Balance -= amount;
                AddTransaction(TransactionType.Withdraw, amount);
                Console.WriteLine($"Withdrew {amount:C}. New balance: {Balance:C}");
                return true;
            }
            else
            {
                Console.WriteLine("Withdrawal amount must be positive and less than or equal to the current balance.");
                return false;
            }
        }
        public bool Transfer(BankAccount targetAccount, decimal amount)
        {
            if (Withdraw(amount))
            {
                targetAccount.Deposit(amount);
                AddTransaction(TransactionType.Transfer, amount, targetAccount.AccountNumber);
                Console.WriteLine($"Transferred {amount:C} to account {targetAccount.AccountNumber}. New balance: {Balance:C}");
                return true;
            }
            else
            {
                Console.WriteLine("Transfer failed due to insufficient funds.");
                return false;
            }
        }
        private void AddTransaction(TransactionType type, decimal amount, int? target = null)
        {
            Transactions.Add(new Transaction(type, amount, target));
        }
        public void ShowBankReport()
        {
            Console.WriteLine($"Bank: {bankName}, Branch: {branchCode}");
            Console.WriteLine($"Total Customers: {customers.Count}");
            Console.WriteLine($"Total Accounts: {customers.Sum(c => c.Accounts.Count)}");
            Console.WriteLine($"Total Balance: {customers.Sum(c => c.Accounts.Sum(a => a.Balance)):C}");
        }
    }
}
