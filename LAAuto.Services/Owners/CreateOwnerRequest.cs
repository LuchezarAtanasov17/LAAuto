namespace LAAuto.Services.Owners
{
    public class CreateOwnerRequest
    {
        public string Username { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;
    }
}
