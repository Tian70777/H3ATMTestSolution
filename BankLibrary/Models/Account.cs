
namespace BankLibrary.Models
{
    public class Account
    {
        public Guid AccountId { get; set; } //PK
        public string AccountNumber { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public string AccountName { get; set; } = string.Empty ;
        // Use init accessor for read-only after initialization
        // can initialize an Account object with the AccountNumber property
        // set during object creation, and it cannot be changed afterward.

        // Foreign key for User
        public Guid OwnerId { get; set; }
        //navigation property
        public User Owner { get; set; }

        // navigation property for Cards and transactions
        public List<Card> LinkedCards { get; set; } = new List<Card>();
        public List<Transaction> Transactions { get; set; } = new List<Transaction> ();
        protected Account() { }
        public Account(string accountNumber, decimal initialBalance, string accountName, User owner)
        {
            AccountId = Guid.NewGuid();
            AccountNumber = accountNumber;
            Balance = initialBalance;
            AccountName = accountName;
            Owner = owner;
            OwnerId = owner.UserId;

            //owner.AddAccount(this);
            // do not do it here, avoid coupling between user and account
        }

        // Now, BankAccount is a pure data model (no business logic).
        // It exposes UpdateBalance() only for AccountService to modify the balance.
        internal void UpdateBalance(decimal amount)
        {
            Balance += amount; // Used internally by AccountService
        }

        // Link a card to the account
        public void AddCardToAccount(Card card)
        {
            if (!LinkedCards.Contains(card))
            {
                LinkedCards.Add(card);
                card.LinkedAccount = this;
                card.LinkedAccountId = this.AccountId;
            }
        }

        // Add a card to the account
        public void AddTransactionToAccount(Transaction transaction)
        {
            if (!Transactions.Contains(transaction))
            {
                Transactions.Add(transaction);
                transaction.Account = this;
                transaction.AccountId = this.AccountId;
            }
        }
    }
}
