using oop_task_again;
using System;
namespace again
{
    class Program
    {
        static void Main(string[] args)
        {
            BankAccount account1 = new BankAccount();
            account1.ShowAccountDetails();
            BankAccount account2 = new BankAccount("agks", "12345678912345", "01234567890", "suhag", 9650,"Ahly","BR003");
            account2.ShowAccountDetails();
            BankAccount account = new BankAccount("bns", "96385274195137", "01134741892", "aswan","qnb","BR007");
            account.ShowAccountDetails();
            BankAccount savingAccount1 = new SavingAccount("sfw","95175385245612","01478529636","qena",7892,0.3m,"masr","BR006");
            BankAccount currentAccount1 = new CurrentAccount("ofll","14526378921537","01378626912","luxor",4651,900,"adib","BR008");//bank fields+method+ctor current
          //  currentAccount1.OverdraftLimit = 300;
            List<BankAccount> bankAccounts = new List<BankAccount>();
            bankAccounts.Add(savingAccount1);
            bankAccounts.Add(currentAccount1);
            foreach (BankAccount bankAccount in bankAccounts)
            {
                bankAccount.ShowAccountDetails();
                bankAccount.CalculateInterest();
            }
        }
    }
}