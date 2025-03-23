
namespace BankLibrary.Models
{
    public class Transaction
    {
        public Guid TransactionId { get; set; }
        public DateTime Timestamp { get; set; }
        public decimal Amount { get; set; }
        public string TransactionType { get; set; } = string.Empty; // Deposit, Withdrawal, Transfer
        public string Source { get; set; } = string.Empty; //  ATM, Online Banking, Branch, etc.

        public Guid AccountId { get; set; } // FK for account
        public Account Account { get; set; } // nav prop for Account

        public Transaction() { } // Parameterless constructor for EF

        public Transaction(decimal amount, string transactionType, string source, Account account)
        {
            TransactionId = Guid.NewGuid();
            Amount = amount;
            TransactionType = transactionType;
            Source = source;
            Account = account;
            AccountId = account.AccountId;
            Timestamp = DateTime.Now;
        }
    }
}
