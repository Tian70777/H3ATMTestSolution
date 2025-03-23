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
        }

        public User 
    }
}
