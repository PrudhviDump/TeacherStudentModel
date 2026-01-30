using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BackEnd.Models
{
    public class StockEntry
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;

        // Date when stock was received
        [BsonElement("receivedDate")]
        public DateTime ReceivedDate { get; set; } = DateTime.UtcNow;

        // Total amount spent for the entire stock (₹)
        [BsonElement("totalAmountSpent")]
        public decimal TotalAmountSpent { get; set; }

        // List of items received in this stock
        [BsonElement("items")]
        public List<StockEntryItem> Items { get; set; } = new();
    }
}
