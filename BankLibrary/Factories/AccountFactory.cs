using BankLibrary.Models;
namespace BankLibrary.Factories
{
    public static class AccountFactory
    {
        public static Account CreateAccount(string type, string accountNumber, decimal initialBalance, User owner)
        {
            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentException("Account type cannot be empty or whitespace.");

            return type.Trim().ToLower() switch
            {
                "savings" => new Account (accountNumber, initialBalance,  "Savings",  owner),
                "checking" => new Account(accountNumber, initialBalance, "Checking", owner),
                "joint" => new Account(accountNumber, initialBalance, "Joint Account", owner),
                _ => throw new ArgumentException($"Invalid account type: {type}")
            };
        }
    }
}
