using BankLibrary.Factories;
using BankLibrary.Models;
using BankLibrary.Test.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BankLibrary.Test.UnitTests.ModelTests
{
    public class AccountTests
    {
        //[Fact]
        //public void AddCardToAccount_ShouldAddCardToAccount()
        //{
        //    // Arrange
        //    var user = new User("John Doe");
        //    var account = AccountFactory.CreateAccount("savings", "A123", 1000m, user);
        //    var card = CardFactory.CreateCard("credit", "1234123412341234", "1414", account);
           

        //    // Act
        //    account.AddCardToAccount(card);

        //    // Assert
        //    Assert.Contains(card,account.LinkedCards);
        //    Assert.Equal(account.AccounId, card.LinkedAccountId);
        //    Assert.Equal(account, card.LinkedAccount);
        //}

        [Fact]
        public void FakerShouldGenerateValidObjects_ShouldAddTwoAccountsToUser()
        {
            // Arrange: Generate a fake user, account, and card
            
            var user = FakeDataGenerator.GenerateFakeUser();

            // Act
            var account1 = FakeDataGenerator.GenerateFakeAccount(user);
            var account2 = FakeDataGenerator.GenerateFakeAccount(user);

            // Assert: Validate the generated user
            System.Diagnostics.Debug.WriteLine($"Account 1: {account1.AccountNumber}");
            System.Diagnostics.Debug.WriteLine($"Account 2: {account2.AccountNumber}");

            // user has exactly 2 accounts, with different accoung nr
            Assert.Equal(2, user.Accounts.Count);
            Assert.NotEqual(account1.AccountNumber, account2.AccountNumber);
            Assert.NotEqual(Guid.Empty, account1.AccountId);
        }

    }
}
