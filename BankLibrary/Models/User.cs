

namespace BankLibrary.Models
{
    public class User
    {
        public Guid UserId { get; set; }  //PK . initialize when creating user in constructor
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public int BankId { get; set; }//FK for bank
        public Bank Bank { get; set; }//Nav prop

        // Navigation property for Accounts
        public List<Account> Accounts { get; } =  new List<Account>();

        

        // Parameterless constructor for EF Core
        public User() { }

        // Constructor for creating instances in application code
        public User(string userName)
        {
            UserId = Guid.NewGuid();
            UserName = userName;
        }

        // Method to add an account
        public void AddAccount(Account account)
        {
            if (account != null && !Accounts.Contains(account))
            {
                Accounts.Add(account);
                account.Owner = this;
                account.OwnerId = this.UserId;
            }
        }
    }
}
