using Xunit;
using BankLibrary.Data;
using BankLibrary.Models;
using BankLibrary.Test.Helpers;
using Xunit.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace BankLibrary.Test.IntegrationTests
{
    public class UserIntegrationTests
    {
        private readonly ITestOutputHelper _output;

        public UserIntegrationTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void CanCreateAndRetrieveUser()
        {
            // Arrange
            using var context = TestDbContextFactory.Create();
            var user = FakeDataGenerator.GenerateFakeUser();

            // Act
            context.Users.Add(user);
            context.SaveChanges();

            // Assert
            var retrievedUser = context.Users.FirstOrDefault(u => u.UserId== user.UserId);
            
            Assert.NotNull(retrievedUser);
            Assert.Equal(user.UserName, retrievedUser.UserName);

            // Display all users in the database (optional for debugging)
            _output.WriteLine("User retrieved successfully!");
            _output.WriteLine($"User ID: {retrievedUser.UserId}");
            _output.WriteLine($"User Name: {retrievedUser.UserName}");
            _output.WriteLine($"User Email: {retrievedUser.Email}");

            _output.WriteLine($"Bank info: {user.Bank.BankName}");
        }

        [Fact]
        public void CanSaveMultipleUsersAndRetrieveAllUser()
        {
            // Arrange
            using var context = TestDbContextFactory.Create();
            var users = FakeDataGenerator.GenerateFakeUsers(10);

            // Act
            context.Users.AddRange(users);
            context.SaveChanges();
            var allUsers = context.Users.ToList();

            // Assert
            Assert.Equal(10, allUsers.Count);
            foreach (var user in allUsers)
            {
                _output.WriteLine($"User ID: {user.UserId}");
                _output.WriteLine($"User Name: {user.UserName}");
                _output.WriteLine($"User Email: {user.Email}");
            }
            
        }
    }
}
