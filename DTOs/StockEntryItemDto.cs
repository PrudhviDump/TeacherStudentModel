using System.ComponentModel.DataAnnotations;

namespace BackEnd.DTOs
{
    public class StockEntryItemDto
    {
        [Required]
        public string ProductId { get; set; } = null!;

        [Range(0.01, double.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        public decimal QuantityKg { get; set; }
    }
}
