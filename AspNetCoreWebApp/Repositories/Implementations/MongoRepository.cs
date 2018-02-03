using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using AspNetCoreWebApp.Repositories.Interfaces;
using AspNetCoreWebApp.Models.Interfaces;
using AspNetCoreWebApp.Models.Configuration;
using MongoDB.Driver;
using MongoDB.Bson.Serialization;
namespace AspNetCoreWebApp.Data.Repositories.Implmentations
{
    public class MongoRepository<T> : IDocumentRepository<T>, IDisposable
    {
        private MongoClient _client;
        private IMongoDatabase _database;
        private IMongoCollection<T> _collection;
        private MongoOptions _options;
        private ICacheRepository _cache;
        private ILogger _logger;
        public MongoRepository(IOptions<MongoOptions> optionsAccessor,
                               ILogger<MongoRepository<T>> logger,
                               ICacheRepository cache)
        {
            Options = optionsAccessor.Value;
            Logger = logger;
            Logger.LogInformation($"MongoDB Options: {Options.Uri} {Options.Database}:{Options.Collection}");
            Cache = cache;
            Client = new MongoClient(Options.Uri);
            Database = Client.GetDatabase(Options.Database);
            Collection = Database.GetCollection<T>(Options.Collection);

            // Register implementations of interfaces
            // TODO: Make this automatice on T
            var interfaceTypes = Assembly.GetAssembly(typeof(T))
                    .GetTypes()
                    .Where(type => type.IsInterface);
            foreach(var interfaceType in interfaceTypes)
            {
              var implementationTypes = AppDomain.CurrentDomain.GetAssemblies()
                            .SelectMany(s => s.GetTypes())
                            .Where(p => interfaceType.IsAssignableFrom(p) && !p.IsInterface);
              foreach(var implementationType in implementationTypes)
              {
                Logger.LogInformation($"Registering {implementationType}");
                if(BsonClassMap.IsClassMapRegistered(implementationType)) continue;
                BsonClassMap.RegisterClassMap(new BsonClassMap(implementationType));
              }
            }
        }

        public IMongoCollection<T> Collection { get => _collection; set => _collection = value; }
        public IMongoDatabase Database { get => _database; set => _database = value; }
        public MongoClient Client { get => _client; set => _client = value; }
        public MongoOptions Options { get => _options; set => _options = value; }
        public ILogger Logger { get => _logger; set => _logger = value; }
        public ICacheRepository Cache { get => _cache; set => _cache = value; }

        public async Task<IList<T>> Find(Expression<Func<T, bool>> query)
        {
          //Logger.LogInformation($"MongoDB Finding");
           // Return the enumerable of the collection
          return await Collection.Find<T>(query).ToListAsync();
        }
        public async Task Insert(T entity)
        {
          //Logger.LogInformation($"MongoDB Inserting {Newtonsoft.Json.JsonConvert.SerializeObject(entity, Newtonsoft.Json.Formatting.Indented)}");
          await Collection.InsertOneAsync(entity);
        }
        public async Task Insert(IEnumerable<T> list)
        {
          throw new NotImplementedException();
        }
        public void Dispose()
        {
            //_client?.Dispose();
        }

    }

}
