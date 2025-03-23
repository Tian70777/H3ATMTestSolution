using BankLibrary.Models;

namespace BankLibrary.Factories
{
    public static class CardFactory
    {
        public static Card CreateCard(string type, string cardNumber, string pin, string cvv, Account linkedAccount)
        {
            return type.ToLower() switch
            {
                "debit" => new Card (cardNumber, pin, cvv, "Debit", DateTime.Now.AddYears(3), linkedAccount),
                "credit" => new Card (cardNumber, pin,cvv, "Credit", DateTime.Now.AddYears(5), linkedAccount),
                "virtual" => new Card (cardNumber, pin, cvv, "Virtual", DateTime.Now.AddYears(1) , linkedAccount),
                _ => throw new ArgumentException("Invalid card type")
            };
        }
    }
}
