using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SantaClouse
{

    public static class MongoDBConfig
    {
        public static MongoClientSettings Settings { get; set; }
        public static string DBName
        {
            get
            {
                return ConfigurationManager.AppSettings["DBName"];
            }
        }

        public static string Username
        {
            get
            {
                return ConfigurationManager.AppSettings["Username"];
            }
        }

        public static string Password
        {
            get
            {
                return ConfigurationManager.AppSettings["Password"];
            }
        }

        public static string Host
        {
            get
            {
                return ConfigurationManager.AppSettings["Host"];
            }
        }

        public static int Port
        {
            get
            {
                int port;
                int.TryParse(ConfigurationManager.AppSettings["Port"], out port);
                return port;
            }
        }

        public static string ConnectionString
        {
            get
            {
                return $"mongodb://{Username}:{Password}@{Host}:{Port}/{DBName}";
            }
        }
    }
}
