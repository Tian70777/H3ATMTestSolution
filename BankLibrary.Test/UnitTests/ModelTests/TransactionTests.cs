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
    public class TransactionTests
    {
        [Fact]
        public void FakeTransaction_ShouldHaveUniqueTransactionIds()
        {
            // Arrange
            var user = FakeDataGenerator.GenerateFakeUser();
            var account = FakeDataGenerator.GenerateFakeAccount(user);

            // Act
            var transaction1 = FakeDataGenerator.GenerateFakeTransaction(account);
            var transaction2 = FakeDataGenerator.GenerateFakeTransaction(account);

            // Assert
            Assert.NotEqual(transaction1.TransactionId, transaction2.TransactionId);
            Assert.NotEqual(Guid.Empty, transaction1.TransactionId);
        }
    }
}
