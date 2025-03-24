using BankLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary.Test.Mocks
{
    public class MockIdentityVerificationService : IIdentityVerificationService
    {
        public Task<bool> VerifyCprAsync(string cprNumber)
        {
            // This is the "fake" logic for testing.
            // Example: Only "111111-1111" is considered valid, everything else is invalid.
            return Task.FromResult(cprNumber == "111111-1111");
        }
    }
}
