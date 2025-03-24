using BankLibrary.Data;
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


        public async Task<User> CreateUserAsync(string name, string email)
        {
            var bank = await  _context.Banks.FirstOrDefaultAsync();
            if (bank == null)
            {
                bank = new Bank { BankName = "Global Bank" };
                await _context.Banks.AddAsync(bank);
                await _context.SaveChangesAsync();
            }

            var user = new User(name) { Email = email, Bank = bank, BankId = bank.BankId };
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

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users
                .Include(u => u.Accounts)
                .ThenInclude(a => a.LinkedCards)
                .ToListAsync() ?? new List<User>();
        }

    }
}
