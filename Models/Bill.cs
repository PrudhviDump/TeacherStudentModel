using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BackEnd.Models
{
    public class Bill
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;

        // Invoice number like 1017
        public int InvoiceNumber { get; set; }

        // Restaurant name (Tulips Grand)
        public string RestaurantName { get; set; } = null!;

        // Invoice date
        public DateTime InvoiceDate { get; set; } = DateTime.UtcNow;

        // All items sold in this bill
        public List<BillItem> Items { get; set; } = new();

        // Final total amount (₹)
        public decimal TotalAmount { get; set; }
    }
}
