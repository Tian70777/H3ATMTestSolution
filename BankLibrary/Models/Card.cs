
namespace BankLibrary.Models
{
    public class Card
    {
        public Guid CardId { get; set; } // PK
        public string CardNumber { get; set; } = string.Empty;
        public string CardType { get; set; } = string.Empty;
        public string Pin { get; set; } = string.Empty; // 4-digit PIN
        public string CVV { get; set; } = string.Empty;  // 3-digit CVV
        public bool IsBlocked { get; set; }
        public DateTime ExpiryDate { get; set; }

        
        public Guid LinkedAccountId { get; set; } //FK for account
       
        public Account LinkedAccount { get; set; }  // nav property


        // Parameterless constructor for EF Core
        public Card() { }

        // Constructor for creating instances in application code
        public Card(string cardNumber, string pin, string cvv, string cardType,DateTime expiryDate, Account account)
        {
            CardId = Guid.NewGuid(); 
            CardNumber = cardNumber;
            Pin = pin;
            CVV = cvv;
            CardType = cardType;
            ExpiryDate = expiryDate;
            LinkedAccount = account;
            LinkedAccountId = account.AccountId;
            IsBlocked = false;
        }

        public bool ValidatePin(string enteredPin)
        {
            return Pin == enteredPin && !IsBlocked;
        }

        public bool ValidateCVV(string enteredCvv)
        {
            return CVV == enteredCvv;
        }
    }
}
