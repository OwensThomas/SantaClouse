using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace SantaClouse
{
    public class MongoDB : IDataBase
    {
        private IMongoDatabase database
        {
            get
            {
                return MongoConnection.Instance.Database;
            }
        }
        public IEnumerable<Order> GetAllOreders()
        {
            IMongoCollection<Order> OrderCollection = database.GetCollection<Order>("orders");
            List<Order> orders = OrderCollection.Find(new BsonDocument()).SortBy(o => o.requestDate).ToList();
            foreach (Order order in orders)
            {
            }
            return orders;
        }

        public IEnumerable<Toy> GetAllToys()
        {
            IMongoCollection<Toy> toysCollection = database.GetCollection<Toy>("toys");
            return toysCollection.Find(new BsonDocument()).SortByDescending(t => t.Name).ToList();
        }
        public Order GetOrder(string id)
        {
            IMongoCollection<Order> orderCollection = database.GetCollection<Order>("orders");
            Order order = orderCollection.Find(_ => _.ID == id).FirstOrDefault();

            return order;
        }

        public User GetUser(User user)
        {
            IMongoCollection<User> userCollection = database.GetCollection<User>("user");
            return userCollection.Find(_ => _.Email == user.Email && _.Password == user.Password).FirstOrDefault();
        }

        public Toy GetToy(string id)
        {
            IMongoCollection<Toy> toyCollection = database.GetCollection<Toy>("toys");
            return toyCollection.Find(_ => _.ID == id).FirstOrDefault();
        }

        public bool UpdateOrder(Order order)
        {
            IMongoCollection<Order> orderCollection = database.GetCollection<Order>("order");
            var filter = Builders<Order>.Filter.Eq("_id", ObjectId.Parse(order.ID));
            var update = Builders<Order>.Update
                .Set("amount", order.TypeStatus);
            try
            {
                orderCollection.UpdateOne(filter, update);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateToy(Toy toy)
        {
            IMongoCollection<Toy> toyCollection = database.GetCollection<Toy>("toys");
            var filter = Builders<Toy>.Filter.Eq("name", toy.Name);
            var update = Builders<Toy>.Update
                .Set("amount", toy.Amount);
            try
            {
                toyCollection.UpdateOne(filter, update);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}