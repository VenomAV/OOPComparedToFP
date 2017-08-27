namespace BankAccountCSharp
{
    public class BankAccount
    {
        public BankAccount()
        {
        }

        public BankAccount(string owner, decimal balance)
        {
            Owner = owner;
            Balance = balance;
        }

        public int Id { get; private set; }
        public string Owner { get; private set; }
        public decimal Balance { get; private set; }

        public void Deposit(decimal amount)
        {
            Balance += amount;
        }

        public void Withdraw(decimal amount)
        {
            Balance -= amount;
        }
    }
}
