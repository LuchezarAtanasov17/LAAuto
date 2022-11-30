namespace LAAuto.Web.Models.Services
{
    public class CreateServiceRequest
    {
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public string Location { get; set; } = null!;

        //public TimeOnly OpenTime { get; set; }

        //public TimeOnly CloseTime { get; set; }
    }
}
