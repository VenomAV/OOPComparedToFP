using System.Data.Entity;

namespace BankAccountCSharp
{
    public class BankAccountContext : DbContext
    {
        public BankAccountContext() : base("DefaultConnection")
        {
        }

        public DbSet<BankAccount> BanckAccounts { get; set; }
    }
}
