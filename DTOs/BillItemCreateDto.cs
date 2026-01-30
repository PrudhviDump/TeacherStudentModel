namespace BackEnd.DTOs
{
    public class BillItemCreateDto
    {
        public string ProductId { get; set; } = null!;
        public string Description { get; set; } = null!;

        public decimal QuantityKg { get; set; }
        public decimal PricePerKg { get; set; }

        // User decides
        public decimal DiscountPercent { get; set; }
    }
}
