using BankLibrary.Factories;
using BankLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BankLibrary.Test.UnitTests.FactoryTests
{
    public class CardFactoryTests
    {
        [Fact]
        public void CreateCard_ShouldReturnDebitCardAndExpiryDate_WhenTypeIsDebit()
        {
            var user = new User("John");
            var account = AccountFactory.CreateAccount("savings", "ACC123", 1000m, user);

            // Act
            var card = CardFactory.CreateCard("debit", "1111-2222-3333-4444", "1234", "233", account);

            // Assert
            Assert.Equal("Debit", card.CardType);
            Assert.Equal("1111-2222-3333-4444", card.CardNumber);
            Assert.Equal(DateTime.Now.AddYears(3).Date, card.ExpiryDate.Date);
        }
    }
}
