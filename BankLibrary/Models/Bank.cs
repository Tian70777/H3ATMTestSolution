

namespace BankLibrary.Models
{
    public class Bank
    {
        public int BankId { get; set; }
        public string BankName { get; set; } = string.Empty;

        // Navigation property for Users
        public List<User> Users { get; } = new List<User>();
        public Bank() { }    
        public Bank(string bankName)
        {
            BankName = bankName;
        }
        public void AddUser(User user)
        {
            if ( user!= null  && !Users.Contains(user))
            { 
                Users.Add(user);
                user.Bank = this;
                user.BankId = this.BankId;
            }
        }
    }
}
