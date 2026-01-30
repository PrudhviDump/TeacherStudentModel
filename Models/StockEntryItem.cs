using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BackEnd.Models
{
    public class StockEntryItem
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("productId")]
        public string ProductId { get; set; } = null!;

        // Quantity received in Kg
        [BsonElement("quantityKg")]
        public decimal QuantityKg { get; set; }
    }
}
