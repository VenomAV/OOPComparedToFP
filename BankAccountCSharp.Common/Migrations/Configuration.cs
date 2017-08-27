namespace BankAccountCSharp.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<BankAccountContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BankAccountContext context)
        {
            context.BanckAccounts.AddOrUpdate(ba => ba.Owner,
                new BankAccount("Alice", 10000),
                new BankAccount("Bob", 5000),
                new BankAccount("Charlie", 12000),
                new BankAccount("Dan", 7000));
        }
    }
}
