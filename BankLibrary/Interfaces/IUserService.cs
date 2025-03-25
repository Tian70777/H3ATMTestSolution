using BankLibrary.Models;
 

namespace BankLibrary.Interfaces
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(string name, string email, string password);
        Task<User?> GetUserByIdAsync(Guid userId);
        Task<List<User>> GetAllUsersAsync();
        // bool DeleteUser(Guid userId);
        Task<User?> LoginAsync(string email, string inputPassword);
    }
}
