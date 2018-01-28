using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SantaClouse
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID { get; set; }

        [BsonElement("kid")]
        public string Kid { get; set; }

        [BsonElement("status")]
        public TypeStatus Status { get; set; }

        [BsonElement("toys")]
        public List<Toy> Toys { get; set; }

        [BsonElement("requestDate")]
        public DateTime requestDate { get; set; }

        [BsonElement("toys")]
        public List<ToyKid> ToyKids { get; set; }

        public List<Decimal> PriceRequest { get; set; }
    }

    public class ToyKid
    {
        [BsonElement("name")]

        public string ToyName { get; set; }
    }
}