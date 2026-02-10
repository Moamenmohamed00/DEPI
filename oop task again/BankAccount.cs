using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace oop_task_again
{
    internal class BankAccount
    {
        const string BankCode = "BNK001";
        readonly DateTime CreatedDate;
        private int _accountNumber;
        private string _fullName;
        private string _nationalId;
        private string _phoneNumber;
        private string _address;
        private decimal _balance;
        private static int accountCounter = 0;
        public Bank Bank { get; private set; }//account m-1  bank
        public Customer Customer { get; private set; }//customer 1-m account
        public List<Transaction> Transactions {  get; private set; }
        public int AccountNumber { get => _accountNumber; private set {_accountNumber=value; }  }
        public string FullName {
            get => _fullName;
            set { if (!string.IsNullOrEmpty(value))
                    _fullName = value;
                else
                    Console.WriteLine("please put your name");
            } }
        public string NationalId { get => _nationalId;
           private set { if (IsValidNationalID(value))
                    _nationalId = value;
                else
                    Console.WriteLine("national id is missing digits.please 14 digits");
            } }
        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                if (IsValidPhoneNumber(value))
                    _phoneNumber = value;
                else
                    Console.WriteLine("phone should start with 01 and be 11 digit");
            }
        }
        
    public decimal Balance { get => _balance;
            set { if (value >= 0)
                    _balance = value;
                else
                    Console.WriteLine("we no't accept negative");
            }
        }
        public string? Address { get => _address; set { _address = value; } }
       public BankAccount()
        {
           Bank= new Bank();
            CreatedDate = DateTime.Now;
            FullName = "unknown";
            NationalId = "00000000000000";
            PhoneNumber = "01000000000";
            Balance = 0;
            Address = "unknown";
            AccountNumber=++accountCounter;
        }
        public BankAccount(string fullname,string nationalid,string phonenumber,string address,decimal balance,string Bankname,string Branchcode)
        {
           Bank= new Bank();
            AccountNumber = ++accountCounter;
            CreatedDate= DateTime.Now;
            FullName = fullname;
            NationalId = nationalid;
            PhoneNumber = phonenumber;
            Address = address;
            Balance = balance;
            Bank.BankName= Bankname;
            Bank.BranchCode= Branchcode;
            Transactions = new List<Transaction>();
        }
        public BankAccount(string fullname, string nationalid, string phonenumber, string address, string Bankname, string Branchcode) :this(fullname,nationalid,phonenumber,address,0,Bankname,Branchcode)
        {
            
        }
        public virtual void ShowAccountDetails()
        {
            Console.WriteLine($"AccountNumber:{AccountNumber},FullName:{FullName},NationalID:{NationalId},PhoneNumber:{PhoneNumber},Address:{Address},Balance:{Balance},CreatedAt:{CreatedDate}");
            Console.WriteLine($"BankName:{Bank.BankName},BranchCode:{Bank.BranchCode}");
        }
        public bool IsValidNationalID(string nid)
        {
            //bool is_digit=true;
            //foreach(char c in nid)
            //{
            //    if (!char.IsDigit(c))
            //        is_digit = false;
            //}
            //return nid.Length == 14 &&is_digit;
            return nid.Length == 14 && nid.All(char.IsDigit);
        }
        public bool IsValidPhoneNumber(string phone) => phone.Length == 11 && phone.StartsWith("01")&&phone.All(char.IsDigit);
        public virtual void CalculateInterest()
        {
            Console.WriteLine($"balance:{Balance}");
        }
        public virtual string AccountDetails()
        {
            return $"AccountNumber:{AccountNumber},CurrentBalance:{Balance},dateOpened:{CreatedDate}";
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
        private void AddTransaction(TransactionType type, decimal amount, int? target = null)
        {
            Transactions.Add(new Transaction(type, amount, target));
        }

    }
}