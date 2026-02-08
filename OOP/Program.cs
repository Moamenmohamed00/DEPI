using System;
using System.Net;
namespace OOP
{
    class Program
    {
        static void Main(string[] args)
        {
            BankAccount account1=new BankAccount();
            BankAccount account2=new BankAccount("Mohamed Ali","29801011234567","01012345678","Cairo",5000,"QWE","004");
            BankAccount account3=new BankAccount("Sara Ahmed","30105011234567","01098765432","Alexandria","ASD","003");
            account1.ShowAccountDetails();
            account2.ShowAccountDetails();
            account3.ShowAccountDetails();

            BankAccount saving = new SavingAccount(
            fullname: "Ahmed Ali",
            nationalid:"12345678912345",
            phonenumber:"01143412131",
            address:"sdwsq-dfefe-cw",
            .10m,
            balance: 5000,
            bankname:"QWE",
            branchcode: "006"
            );
            BankAccount current = new CurrentAccount("sdw","78945612395137","01234567893","wfef-5ef-ddf3",500,3000,"ASD","003");

            List<BankAccount> accounts = new List<BankAccount>();
            accounts.Add(saving);
            accounts.Add(current);
            foreach (var account in accounts)
            {
                account.ShowAccountDetails();
            }

            Customer c = new Customer("Ahmed Ali", "29801011234567", new DateTime(1999, 5, 10));
            BankAccount acc6 = new CurrentAccount(c.FullName,c.NationalID, "01012345678","alex",.20m,6050,"QWE","006");
                c.Accounts.Add(acc6);
                c.Accounts[0].ShowAccountDetails();


            BankAccount bank = new BankAccount("National Bank", "BR001");

            Customer c1 = bank.AddCustomer("Ahmed Ali", "29801011234567", new DateTime(1998, 1, 1));
            Customer c2 = bank.AddCustomer("Sara Ibrahim", "30101011234567", new DateTime(2001, 5, 10));

            BankAccount acc1 = new SavingAccount("Ahmed Ali", "29801011234567", "01012345678", "Cairo", 0.08m, 2000, bank.bankName, bank.branchCode);
            c1.Accounts.Add(acc1);
acc1.ShowAccountDetails();
            var searchName = bank.Serachbyname("Ahmed");
            var searchNID = bank.Serachbyid("30101011234567");

            Console.WriteLine("\nSearch by name (Ahmed):");
            foreach (var cust in searchName)
                Console.WriteLine(cust);

            Console.WriteLine("\nSearch by National ID:");
            Console.WriteLine(searchNID);

           Console.WriteLine(bank.updateCustomer(c1.CustomerID, "Ahmed Mohamed Ali", new DateTime(1998, 1, 1)));

          Console.WriteLine(bank.RemoveCustomer(c1.CustomerID)); 

         Console.WriteLine(bank.RemoveCustomer(c2.CustomerID));
            var acc2 = new CurrentAccount("Sara Ibrahim", "30101011234567", "01098765432", "Alexandria", 1000, 3000, bank.bankName, bank.branchCode);

            acc1.Deposit(500);
            acc1.Withdraw(200);

            acc2.Withdraw(2300); 

            acc1.Transfer(acc2, 300); 

            acc1.ShowAccountDetails();
            acc2.ShowAccountDetails();
            bank.ShowBankReport();
            bank.ShowBankReport();
        }
    }
}