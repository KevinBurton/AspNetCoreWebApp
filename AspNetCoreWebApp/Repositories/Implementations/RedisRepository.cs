using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using AspNetCoreWebApp.Repositories.Interfaces;
using AspNetCoreWebApp.Models.Configuration;

namespace AspNetCoreWebApp.Data.Repositories.Implmentations
{
    // Error CS0433: The type 'ConnectionMultiplexer' exists in both 'StackExchange.Redis.StrongName, Version=1.2.4.0, Culture=neutral, PublicKeyToken=c219ff1ca8c2ce46' and 'StackExchange.Redis, Version=1.2.6.0, Culture=neutral, PublicKeyToken=null'
    public class RedisRepository : ICacheRepository, IDisposable
    {
        public RedisRepository(IOptions<RedisOptions> optionsAccessor,
                               ILogger<RedisRepository> logger)
        {
            Options = optionsAccessor.Value;
            Logger = logger;
            Logger.LogInformation($"Redis Options: {Options.Server}:{Options.Port}");
            try
            {
              Redis = StackExchange.Redis.ConnectionMultiplexer.Connect(Options.Server + ",allowAdmin=true");
            }
            catch(RedisConnectionException e)
            {
              Logger.LogError(new EventId(1), e, "Unable to connect");
            }
        }

        public RedisOptions Options { get; private set; }
        public ILogger Logger { get; private set; }
        public ConnectionMultiplexer Redis { get; private set; }

        public Dictionary<string, List<KeyValuePair<string, string>>> Info(string section)
        {
          if(Redis != null)
          {
            var server = Redis.GetServer(Options.Server, Options.Port);
            return server.Info(section).ToDictionary(group => group.Key, group => group.ToList());
          }
          return new Dictionary<string, List<KeyValuePair<string, string>>>();
       }
        public void Dispose()
        {
            //_client?.Dispose();
        }
    }
}
