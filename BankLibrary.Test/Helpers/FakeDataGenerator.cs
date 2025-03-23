using BankLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;

namespace BankLibrary.Test.Helpers
{
    public static class FakeDataGenerator
    {
        private static Bank _fakeBank;
        // Generate a Fake Bank
        public static Bank GetOrCreateFakeBank()
        {
            if (_fakeBank == null)
            {
                Console.WriteLine("Creating a new fake bank...");
                _fakeBank = GenerateFakeBank();
            }
            else
            {
                Console.WriteLine("Reusing existing fake bank...");
            }
            return _fakeBank;
        }
        public static Bank GenerateFakeBank()
        {
            var bankFaker = new Faker<Bank>()
                .RuleFor(b => b.BankId, f => f.Random.Int(1, 100)) // Generate random Bank ID
                .RuleFor(b => b.BankName, f => f.Company.CompanyName()); // Realistic bank name

            return bankFaker.Generate();
        }

        // Generate a Fake Account with with a required bank parameter
        public static User GenerateFakeUser()
        {
            var bank = GetOrCreateFakeBank();

            if (bank == null) throw new ArgumentNullException(nameof(bank), "Bank cannot be null.");

            var userFaker = new Faker<User>()
                .CustomInstantiator(f => new User(
                    f.Name.FullName() // Generate realistic name
                 ))
                .RuleFor(u => u.UserId, f => Guid.NewGuid()) // Generate unique User ID
                .RuleFor(u => u.Email, f => f.Internet.Email()) // Generate a fake email
                .RuleFor(u => u.Bank, f => bank) // assign bank
                .RuleFor(u => u.BankId, f =>  bank.BankId);

            var user = userFaker.Generate();
            // Add the uuse to the bank
            bank.AddUser(user);

            return user;
        }

        // Generate Multiple Fake Users with a required bank parameter
        public static List<User> GenerateFakeUsers(int count)
        {
            var bank = GetOrCreateFakeBank();

            if (bank == null) throw new ArgumentNullException(nameof(bank), "Bank cannot be null.");
            if (count <= 0) throw new ArgumentException("User count must be greater than zero.");

            var users = new List<User>();

            for (int i = 0; i < count; i++)
            {
                var user = GenerateFakeUser(); // Call the single user generator
                users.Add(user); // Add generated user to the list
            }

            return users;
        }


        // Generate a Fake Account with with a required user parameter
        public static Account GenerateFakeAccount(User owner)
        {
            if(owner == null) throw new ArgumentNullException(nameof(owner), "User cannot be null.");

            string accountNumber;
            do
            {
                // Generate a new random account number
                accountNumber = new Faker().Finance.Account(10);
            }
            while (owner.Accounts.Any(a => a.AccountNumber == accountNumber)); // Ensure uniqueness


            var accountFaker = new Faker<Account>()
                .CustomInstantiator(f => new Account(
                    accountNumber,                 // unique number
                    f.Finance.Amount(1000, 10000), // Initial balance
                    f.Commerce.ProductName(),      // Account name
                    owner                          // Owner
                ))
                .RuleFor(a => a.AccountId, f => Guid.NewGuid()) // Generate unique Account ID
                .RuleFor(a => a.AccountNumber, f => accountNumber) // Random 10-digit account number
                .RuleFor(a => a.Balance, f => f.Finance.Amount(100, 10000)) // Random balance between 100 and 10000
                .RuleFor(a => a.AccountName, f => f.PickRandom(new[] { "Savings", "Checking", "Joint Account" })) // Random card type
                .RuleFor(a => a.Owner, f => owner) //link to the passed user
                .RuleFor(a => a.OwnerId, f => owner.UserId); // link the userId
            
            var account = accountFaker.Generate();
            owner.AddAccount(account);

            return account;
        }

        public static Card GenerateFakeCard(Account account)
        {
            if (account == null) throw new ArgumentNullException(nameof(account), "Account cannot be null.");

            // Generate a unique card number to avoid duplicates
            string cardNumber;
            do
            {
                cardNumber = new Faker().Finance.CreditCardNumber();
            }
            while (account.LinkedCards.Any(c => c.CardNumber == cardNumber));

            var cardFaker = new Faker<Card>()
                .RuleFor(c => c.CardId, f => Guid.NewGuid()) // Generate unique Card ID
                .RuleFor(c => c.CardNumber, f => cardNumber)
                .RuleFor(c => c.Pin, f => f.Random.String2(4, "0123456789"))  // 4-digit PIN
                .RuleFor(c => c.CVV, f => f.Random.String2(3, "0123456789"))  // 3-digit CVV
                .RuleFor(c => c.CardType, f => f.PickRandom(new[] { "Debit", "Credit", "Virtual" })) // Random card type
                .RuleFor(c => c.ExpiryDate, f => f.Date.Future(3)) // 3 years from now
                .RuleFor(c => c.IsBlocked, f => false) // Default to not blocked
                .RuleFor(c => c.LinkedAccount, f => account)
                .RuleFor(c => c.LinkedAccountId, f => account.AccountId);

            var card = cardFaker.Generate();
            account.AddCardToAccount(card);

            return card;
        }

        public static Transaction GenerateFakeTransaction(Account account)
        {
            if (account == null) throw new ArgumentNullException(nameof(account), "Account cannot be null.");

            var transactionFaker = new Faker<Transaction>()
                .RuleFor(t => t.TransactionId, f => Guid.NewGuid()) // Generate unique ID
                .RuleFor(t => t.Timestamp, f => DateTime.Now) // timestamp for now
                .RuleFor(t => t.Amount, f => f.Finance.Amount(-1000, 1000)) // Deposit or Withdrawal
                .RuleFor(t => t.TransactionType, f => f.PickRandom(new[] { "Deposit", "Withdrawal"}))
                .RuleFor(t => t.Source, f => f.PickRandom(new[] { "ATM" }))
                .RuleFor(t => t.AccountId, f => account.AccountId)
                .RuleFor(t => t.Account, f => account);

            var transaction = transactionFaker.Generate();
            account.AddTransactionToAccount(transaction);

            return transaction;
        }
    }
}
