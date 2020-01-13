using superdigital.conta.model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace superdigital.conta.data
{
    public class MongoContext
    {
        public MongoContext(IOptions<AppConnectionSettings> appSettings)
        {
            this.urlConexao = appSettings.Value.DefaultConnection;
            this.db = appSettings.Value.Db;
        }

        readonly string urlConexao;
        readonly string db;

        public IMongoDatabase getDatabase()
        {
            var client = new MongoClient(this.urlConexao);
            return client.GetDatabase(this.db);
        }
    }
}
