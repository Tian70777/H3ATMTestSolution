using BankLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary.Interfaces
{
    public interface IAccountService
    {
        Task<Account> CreateAccountAsync(Guid userId, string accountNumber, string accountName, decimal balance);
        Task<List<Account>> GetUserAccountsAsync(Guid userId);
        Task<bool> DepositAsync(Guid accountId, decimal amount);
        Task<bool> WithdrawAsync(Guid accountId, decimal amount);
    }
}
