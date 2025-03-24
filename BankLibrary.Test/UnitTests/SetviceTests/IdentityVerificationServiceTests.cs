using BankLibrary.Test.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BankLibrary.Test.UnitTests.SetviceTests
{
    public class IdentityVerificationServiceTests
    {
        [Fact]
        public async Task VerifyCprAsync_ReturnsTrue_ForValidCpr()
        {
            // Arrange: Use the mock gateway.
            var mockService = new MockIdentityVerificationService();

            // Act: Test a "valid" CPR number.
            bool result = await mockService.VerifyCprAsync("111111-1111");

            // Assert: Expect it to return true.
            Assert.True(result);
        }

        [Fact]
        public async Task VerifyCprAsync_ReturnsFalse_ForInvalidCpr()
        {
            // Arrange: Use the mock gateway.
            var mockService = new MockIdentityVerificationService();

            // Act: Test an "invalid" CPR number.
            bool result = await mockService.VerifyCprAsync("999999-9999");

            // Assert: Expect it to return false.
            Assert.False(result);
        }
    }
}
