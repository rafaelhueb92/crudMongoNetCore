using crudMongoDB.models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crudMongoDB.Data.Context
{
    public class cxMongoDB
    {

        public static string ConnectionString { get; set; }
        public static string DataBaseName { get; set; }
        public static bool IsSSL { get; set; }

        public IMongoDatabase _database { get; set; }

        public cxMongoDB()
        {
            try
            {
                MongoClientSettings settings =  MongoClientSettings.FromUrl(new MongoUrl(ConnectionString));
                if (IsSSL)
                    settings.SslSettings = new SslSettings
                    { EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12 };
                var mongoClient = new MongoClient(settings);
                _database = mongoClient.GetDatabase(DataBaseName);

            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível conectar");
            }
        }

        public IMongoCollection<User> User
        {
            get
            {
                return _database.GetCollection<User>("User");
            }
        }

    }

}
