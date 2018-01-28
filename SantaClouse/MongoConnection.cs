using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SantaClouse
{
    internal sealed class MongoConnection
    {
        private static MongoConnection instance;
        public IMongoDatabase Database { get; private set; }
        private void connect()
        {
            MongoClientSettings settings = new MongoClientSettings();

            // settings.Credentials
            MongoClient client = new MongoClient(MongoDBConfig.ConnectionString);
            IMongoDatabase db = client.GetDatabase(MongoDBConfig.DBName);
            Database = db;
        }

        private static object syncRoot = new object();

        private MongoConnection() { }
        public static MongoConnection Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new MongoConnection();
                            instance.connect();
                        }
                    }
                }

                return instance;
            }
        }


    }
}