namespace BackEnd.DTOs
{
    public class StockBalanceDto
    {
        public string ProductId { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public decimal TotalReceivedKg { get; set; }
        public decimal TotalSoldKg { get; set; }
        public decimal AvailableKg { get; set; }
    }
}
