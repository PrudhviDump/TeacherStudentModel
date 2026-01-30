using System.ComponentModel.DataAnnotations;

namespace BackEnd.DTOs
{
    public class StockEntryCreateDto
    {
        [Required]
        [MinLength(1, ErrorMessage = "At least one stock item is required")]
        public List<StockEntryItemDto> Items { get; set; } = new();

        [Range(0.01, double.MaxValue, ErrorMessage = "Total amount spent must be greater than 0")]
        public decimal TotalAmountSpent { get; set; }
    }
}
