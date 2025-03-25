namespace Bank.Api.DTOs
{
    public class AccountDTO
    {
        public Guid AccountId { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public string AccountName { get; set; }
    }
}
