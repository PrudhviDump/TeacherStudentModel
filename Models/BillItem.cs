using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BackEnd.Models
{
    public class BillItem
    {
        // Which product was sold
        [BsonRepresentation(BsonType.ObjectId)]
        public string ProductId { get; set; } = null!;

        // Text shown in bill (ex: Prawns 31/40)
        public string Description { get; set; } = null!;

        // Quantity in Kg
        public decimal QuantityKg { get; set; }

        // Selling price per Kg (entered during billing)
        public decimal PricePerKg { get; set; }

        // Discount % (keep 0 for now)
        public decimal DiscountPercent { get; set; }
    }
}
