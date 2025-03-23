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
    public class CardTests
    {
        [Fact]
        public void GenerateFakeCard_ShouldGenerateValidCard_WithCorrectLength()
        {
            // Arrange
            
            var user = FakeDataGenerator.GenerateFakeUser();
            var account = FakeDataGenerator.GenerateFakeAccount(user);

            // Act
            var card = FakeDataGenerator.GenerateFakeCard(account);

            // Assert
            Assert.NotNull(card);
            Assert.Equal(4, card.Pin.Length);   // Check PIN length
            Assert.Equal(3, card.CVV.Length);   // Check CVV length
            Assert.False(card.IsBlocked);       // Card should not be blocked
            Assert.Equal(account.AccountId, card.LinkedAccountId);  // Check account link
            Assert.Contains(card, account.LinkedCards);  // Card should be in the account's card list
            Assert.NotEqual(Guid.Empty, card.CardId);
        }

        [Fact]
        public void Card_ValidatePin_ShouldReturnTrue_WhenCorrectPinIsGiven()
        {
            // Arrange
            var bank = new Bank();
            var user = FakeDataGenerator.GenerateFakeUser();
            var account = FakeDataGenerator.GenerateFakeAccount(user);
            var card = FakeDataGenerator.GenerateFakeCard(account);

            // Act
            bool isValid = card.ValidatePin(card.Pin);

            // Assert
            Assert.True(isValid);  // Should be true for the correct PIN
        }

        [Fact]
        public void Card_ValidatePin_ShouldReturnFalse_WhenIncorrectPinIsGiven()
        {
            // Arrange
            var bank = new Bank();
            var user = FakeDataGenerator.GenerateFakeUser();
            var account = FakeDataGenerator.GenerateFakeAccount(user);
            var card = FakeDataGenerator.GenerateFakeCard(account);

            // Act
            bool isValid = card.ValidatePin("9999");  // Deliberately wrong PIN

            // Assert
            Assert.False(isValid);  // Should be false for the wrong PIN
        }


        [Fact]
        public void ValidatePin_ShouldReturnFalse_WhenCardIsBlocked()
        {
            // Arrange
            var bank = new Bank();
            var user = FakeDataGenerator.GenerateFakeUser();
            var account = FakeDataGenerator.GenerateFakeAccount(user);
            var card = FakeDataGenerator.GenerateFakeCard(account);
            card.IsBlocked = true;

            // Act
            bool isValid = card.ValidatePin("1234");

            // Assert
            Assert.False(isValid);
        }
    }
}
