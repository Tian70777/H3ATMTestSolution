using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BankLibrary.Data;
using BankLibrary.Interfaces;
using BankLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace BankLibrary.Services
{
    public class AccountService : IAccountService
    {
        private readonly BankContext _context;

        public async Task<Account> CreateAccountAsync(Guid userId, string accountNumber, string accountName, decimal balance)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
                throw new ArgumentException("User not found");

            // Check if the account number already exists in the Accounts table
            bool accountExists = await _context.Accounts.AnyAsync(a => a.AccountNumber == accountNumber);
            if (accountExists)
                throw new ArgumentException("Account number already exists");


            var account = new Account(accountNumber, balance, accountName, user);

            // Add the account to the user's account list using the AddAccount method
            user.AddAccount(account);

            // Save both the user and the account
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            return account;
        }

        public async Task<bool> DepositAsync(Guid accountId, decimal amount)
        {
            var account = await _context.Accounts.FindAsync(accountId);

            if (account == null)
                throw new ArgumentException("Account not found");

            if (amount <= 0)
                throw new ArgumentException("Deposit amount must be greater than zero");

            // Update the balance
            account.UpdateBalance(amount);

            // Create a transaction
            var transaction = new Transaction(amount, "Deposit", "ATM", account);
            account.AddTransactionToAccount(transaction);

            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> WithdrawAsync(Guid accountId, decimal amount)
        {
            var account = await _context.Accounts.FindAsync(accountId);
            if (account == null)
                throw new ArgumentException("Account not found");

            if (amount <= 0)
                throw new ArgumentException("Withdrawal amount must be greater than zero");

            if (account.Balance < amount)
                throw new ArgumentException("Insufficient funds");

            // Update the balance (subtract)
            account.UpdateBalance(-amount);

            // Create a transaction
            var transaction = new Transaction(-amount, "Withdrawal", "ATM", account);
            account.AddTransactionToAccount(transaction);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Account>> GetUserAccountsAsync(Guid userId)
        {
            var user = await _context.Users
                .Include(u => u.Accounts)
                    .ThenInclude(a => a.LinkedCards)
                .FirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null || user.Accounts == null)
                return new List<Account>();

            return user.Accounts.ToList();
        }

    }
}
