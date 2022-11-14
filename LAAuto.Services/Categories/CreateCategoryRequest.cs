namespace LAAuto.Services.Categories
{
    public class CreateCategoryRequest
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;
    }
}