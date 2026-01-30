namespace BackEnd.DTOs
{
    public class BillCreateDto
    {
        public string RestaurantName { get; set; } = null!;
        public DateTime InvoiceDate { get; set; }

        public List<BillItemCreateDto> Items { get; set; } = new();
    }
}
