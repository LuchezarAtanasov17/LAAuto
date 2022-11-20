namespace LAAuto.Web.Models.Users
{
    public class CreateUserRequest
    {
        public Guid Id { get; set; }

        public string UserName { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string PhoneNumber { get; set; }
    }
}
