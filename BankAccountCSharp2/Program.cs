using System;
using System.Linq;

namespace BankAccountCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (BankAccountContext context = new BankAccountContext())
            {
                while (TryWithdrawFromAccount(context)) ;
            }
        }

        private static bool TryWithdrawFromAccount(BankAccountContext context)
        {
            string owner;
            BankAccount bankAccount;

            Console.Write("Type owner (or 'q' to exit): ");
            owner = Console.ReadLine();
            if (owner == "q")
            {
                return false;
            }
            bankAccount = TryGetBankAccountByOwner(context, owner);
            if (bankAccount != null)
            {
                WithdrawFromAccount(context, bankAccount);
            }
            return true;
        }

        private static void WithdrawFromAccount(BankAccountContext context, BankAccount bankAccount)
        {
            Console.WriteLine($"{bankAccount.Owner} has {bankAccount.Balance}$");

            Console.Write("How much you want to withdraw? ");
            bankAccount.Withdraw(decimal.Parse(Console.ReadLine()));

            if (ConfirmWithdraw(bankAccount) == "y")
            {
                UpdateBankAccount(context, bankAccount);
            }
        }

        private static BankAccount TryGetBankAccountByOwner(BankAccountContext context, string owner)
        {
            /*** Load detached object ***/
            return context.BanckAccounts.AsNoTracking().FirstOrDefault(ba => ba.Owner == owner);
        }

        private static void UpdateBankAccount(BankAccountContext context, BankAccount bankAccount)
        {
            /*** Attach the modified object to the context ***/
            context.Entry(bankAccount).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }

        private static string ConfirmWithdraw(BankAccount bankAccount)
        {
            Console.Write($"{bankAccount.Owner} new balance will be {bankAccount.Balance}. Confirm operation? [y/n] ");
            return Console.ReadLine();
        }
    }
}
