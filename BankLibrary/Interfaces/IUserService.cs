using BankLibrary.Models;

namespace BankLibrary.Interfaces
{
    public interface IUserService
    {
        User CreateUser(string name, string email);
        User? GetUserById(Guid userId);
        List<User> GetAllUsers();
        bool DeleteUser(Guid userId);
    }
}
