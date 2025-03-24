using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BankLibrary.Interfaces
{
    public interface IIdentityVerificationService
    {
        // Interface defines the method VerifyCprAsync that checks whether a CPR number is valid.
        // Both the real and mock implementations will implement this interface.
        Task<bool> VerifyCprAsync(string cprNumber);
    }
}
