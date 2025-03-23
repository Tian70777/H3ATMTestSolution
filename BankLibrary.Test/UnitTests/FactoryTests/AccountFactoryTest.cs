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
    public class AccountFactoryTest
    {
        // create user, only create,m no nned to return or save
        [Fact]
        public void CreateAccountWithUser_ShouldReturnSavingsAccount_WhenTypeIsSavings()
        {
            var user = new User("John");
            var account = AccountFactory.CreateAccount("savings", "ACC123", 1000m, user);

            Assert.Equal("Savings", account.AccountName);
            Assert.Equal(1000m, account.Balance);
        }

        [Fact]
        public void CreateAccount_ShouldThrowException_WhenTypeIsEmpty()
        {
            // Arrange
            var user = new User("John");

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                AccountFactory.CreateAccount("", "ACC123", 1000m, user));

            Assert.Contains("Account type cannot be empty or whitespace", exception.Message);
        }


        [Fact]
        public void CreateAccount_ShouldThrowException_WhenTypeIsInvalid()
        {
            var user = new User("John");

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                AccountFactory.CreateAccount("invalid", "ACC123", 1000m, user));

            Assert.Contains("Invalid account type", exception.Message);
        }
    }
}

