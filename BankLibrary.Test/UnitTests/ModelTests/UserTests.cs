using BankLibrary.Factories;
using BankLibrary.Models;
using BankLibrary.Services;
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

        [Fact]
        public async Task Login_ShouldIncrementLoginAttempts()
        {
            // Arrange
            var context = TestDbContextFactory.Create();
            var userService = new UserService(context);
            var user = await userService.CreateUserAsync("Test User", "test@example.com", "correctpassword");

            // Act: Attempt to login with a wrong password
            for (int i = 1; i <= 4; i++)
            {
                var result = await userService.LoginAsync(user.Email, "wrongpassword");
                Assert.Null(result);

                // Fetch user again to check attempts count
                var updatedUser = await userService.GetUserByIdAsync(user.UserId);
                Assert.Equal(i, updatedUser.LoginAttempts); // Assert the attempt count matches
            }

            // Check that the user is not locked yet
            var notLockedUser = await userService.GetUserByIdAsync(user.UserId);
            Assert.False(notLockedUser.IsLocked);
        }

        [Fact]
        public async Task Login_ShouldLockUserAfterFiveFailedAttempts()
        {
            // Arrange
            var context = TestDbContextFactory.Create();
            var userService = new UserService(context);
            var user = await userService.CreateUserAsync("Test User", "test@example.com", "correctpassword");

            // Act: Attempt to login with the wrong password 5 times
            for (int i = 0; i < 5; i++)
            {
                var result = await userService.LoginAsync(user.Email, "wrongpassword");
                Assert.Null(result);
            }

            // Assert: User should be locked after 5 failed attempts
            var lockedUser = await userService.GetUserByIdAsync(user.UserId);
            Assert.True(lockedUser?.IsLocked);
        }
    }
}
