namespace LAAuto.Services.Clients
{
    public class UpdateClientRequest
    {
        public string UserName { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string PhoneNumber { get; set; }
    }
}