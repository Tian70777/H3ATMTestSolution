using BankLibrary.Data;
using BankLibrary.Helpers;
using BankLibrary.Interfaces;
using BankLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace BankLibrary.Services
{
    public class UserService : IUserService
    {
        private readonly BankContext _context;

        public UserService(BankContext context)
        {
            _context = context;
            EnsureBankExists(); // Make sure the bank exists during service initialization
        }

        private void EnsureBankExists()
        {
            if (!_context.Banks.Any())
            {
                var bank = new Bank
                {
                    BankName = "Global Bank"
                };
                _context.Banks.Add(bank);
                _context.SaveChanges();
            }
        }


        public async Task<User> CreateUserAsync(string name, string email, string rawPassword)
        {
            var bank = await  _context.Banks.FirstOrDefaultAsync();
            if (bank == null)
            {
                bank = new Bank { BankName = "Global Bank" };
                await _context.Banks.AddAsync(bank);
                await _context.SaveChangesAsync();
            }

            // generate a salt:
            string salt = PasswordHelper.GenerateSalt();

            // Hash rawPassword
            string hashedPassword = PasswordHelper.HashPassword(rawPassword, salt);

            // create a new user
            var user = new User(name) { Email = email, Password = hashedPassword, Salt = salt, Bank = bank, BankId = bank.BankId };
            
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            var user = await _context.Users
                .Include(u => u.Accounts)
                .ThenInclude(a => a.LinkedCards)
                .FirstOrDefaultAsync(u => u.UserId == id);
            if (user == null) return null;
            return user;
        }

        // return dto
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users
                .Include(u => u.Accounts)
                .ThenInclude(a => a.LinkedCards)
                .ToListAsync() ?? new List<User>();
        }

        public async Task<User?> LoginAsync(string email, string password)
        {
            // 1. get user by email
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return null;
                throw new InvalidOperationException("User not found.");
            }

            // 2. check user status
            if (user.IsLocked == true)
            {

                throw new InvalidOperationException("User account is locked.");
            }

            // 3. verify password using PasswordHelper
            string salt = user.Salt;
            string storedPassword = user.Password;

            bool isPasswordValid = PasswordHelper.VerifyPassword(password, storedPassword, salt);

            // check login attempts
            if (!isPasswordValid)
            {
                user.LoginAttempts++;

                if (user.LoginAttempts >= 5)
                {
                    user.IsLocked = true;
                    await _context.SaveChangesAsync();
                    return null;
                    throw new InvalidOperationException("User account locked due to too many failed attempts.");
                }

                // Save changes for failed attempt without locking
                await _context.SaveChangesAsync();
                return null;
            }

            // Reset login attempts on successful login
            user.LoginAttempts = 0;
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
