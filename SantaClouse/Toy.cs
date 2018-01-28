using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SantaClouse
{
    public class Toy
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("cost")]
        public decimal Cost { get; set; }

        [BsonElement("amount")]
        public int Amount { get; set; }
    }
}