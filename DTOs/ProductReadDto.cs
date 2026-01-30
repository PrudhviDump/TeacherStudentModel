namespace BackEnd.DTOs
{
    public class ProductReadDto
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string CategoryId { get; set; } = null!;
        public string CategoryName { get; set; } = null!;
    }
}
