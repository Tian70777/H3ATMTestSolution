namespace Bank.Api.DTOs
{
    public class UserDTO
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<AccountDTO> Accounts { get; set; } = new List<AccountDTO>();
    }
}
