using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using BankLibrary;

namespace ATMTest
{
    public class BankAccountTests
    {
        [Fact]
        public void WithDraw_Should_Reduce_Balance_When_Blance_Enough()
        {
            // arrange
            var account = new Account(1000);

            // act
            bool result = account.Withdraw(500);

            // Assert
            Assert.True(result); // Expect success
            Assert.Equal(500, account.Balance); // Balance should now be 500
        }

        [Fact]
        public void Deposit_Should_Increase_Balance_When_Blance_Enough()
        {
            // arrange
            var account = new Account(1000);

            // act
            bool result = account.Deposit(500);

            // Assert
            Assert.True(result); // Expect success
            Assert.Equal(1500, account.Balance); // Balance should now be 1500
        }
    }
}
