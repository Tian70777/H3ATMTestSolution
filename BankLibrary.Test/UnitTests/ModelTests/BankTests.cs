using Xunit;
using BankLibrary.Models;
using BankLibrary.Test.Helpers;

namespace BankLibrary.Test.UnitTests.ModelTests
{
    public class BankTests
    {
        [Fact]
        public void CanGenerateAFakeBank()
        {
            // Act
            var bank = FakeDataGenerator.GenerateFakeBank();

            // Assert
            Assert.NotNull(bank);
            Assert.NotEqual(0, bank.BankId);
            Assert.False(string.IsNullOrEmpty(bank.BankName));

        }
         
        [Fact]
        public void ShouldAlwaysGetTheSameBank()
        {
            var bank1 = FakeDataGenerator.GetOrCreateFakeBank();
            var bank2 = FakeDataGenerator.GetOrCreateFakeBank();
            Console.WriteLine($"Bank 1 ID: {bank1.BankId}");
            Console.WriteLine($"Bank 2 ID: {bank2.BankId}");
            Assert.Equal(bank1.BankId, bank2.BankId); // They should be the same
        }

    }
}
