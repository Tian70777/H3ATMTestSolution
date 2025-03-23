using BankLibrary.Models;

namespace BankLibrary.Interfaces
{
    public interface IATMService
    {
        bool ValidatePin(Card card, string enteredPin);
        void BlockCard(Card card);
        void Logout(Card card);
        bool WithDraw(Card card, int amount);
        bool Deposit(Card card, int amount);
        decimal CheckBalance(Card card);
    }
}
