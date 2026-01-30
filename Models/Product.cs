using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BackEnd.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;

        [BsonElement("name")]
        public string Name { get; set; } = null!;

        // Reference to Category (Fish / Prawn)
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("categoryId")]
        public string CategoryId { get; set; } = null!;
    }
}
