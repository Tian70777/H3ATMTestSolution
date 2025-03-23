using BankLibrary.Factories;
using BankLibrary.Models;
using BankLibrary.Test.Helpers;
using Xunit;

namespace BankLibrary.Test.UnitTests.ModelTests
{
    public class UserTests
    {

        

        [Fact]
        public void Faker_CreateUserWithFakeBank_ShouldInitializeWithCorrectValues()
        {
            // Arrange
            // var bank = FakeDataGenerator.GenerateFakeBank();
            
            // Act
            var user = FakeDataGenerator.GenerateFakeUser();

            // Assert
            Assert.NotNull(user);
            Assert.NotEqual(Guid.Empty, user.UserId);
            Assert.False(string.IsNullOrEmpty(user.UserName));
            Assert.False(string.IsNullOrEmpty(user.Email));
            Assert.NotNull(user.Bank);
            Assert.NotEqual(0, user.BankId);
            Assert.Equal(user.BankId, user.Bank.BankId);
            Assert.Contains(user, user.Bank.Users);
        }

        [Fact]
        public void CreateUser_ShouldInitializeWithCorrectValues()
        {
            // Arrange
            string userName = "John Doe";

            // Act
            var user = new User(userName);

            // Assert
            Assert.Equal(userName, user.UserName);
            Assert.NotEqual(Guid.Empty, user.UserId);
            Assert.Empty(user.Accounts); // Should start with no accounts
        }

        [Fact]
        public void AddAccount_ShouldAddAccountToUser()
        {
            // Arrange
            var user = new User("John Doe");
            var account = AccountFactory.CreateAccount("savings", "A123", 1000m, user);

            // Act
            user.AddAccount(account);

            // Assert
            Assert.Contains(account, user.Accounts);
            Assert.Equal(user.UserId, account.OwnerId);
            Assert.Equal(user, account.Owner);
        }
    }
}
