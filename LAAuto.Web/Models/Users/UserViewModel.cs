namespace LAAuto.Web.Models.Users
{
    public class UserViewModel
    {
        public Guid Id { get; set; }

        public string UserName { get; set; } = null!;

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string Email { get; set; } = null!;

        public string? PhoneNumber { get; set; }
    }
}
