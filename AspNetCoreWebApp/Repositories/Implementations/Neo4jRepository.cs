using System;
using System.Linq;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using AspNetCoreWebApp.Models.Configuration;
using AspNetCoreWebApp.Repositories.Interfaces;
using Neo4j.Driver.V1;
// https://neo4j.com/developer/dotnet/
namespace AspNetCoreWebApp.Data.Repositories.Implmentations
{
    public class Neo4jMessageRepository : IGraphRepository
    {
        private IDriver _driver;
        private Neo4jOptions _options;
        private Microsoft.Extensions.Logging.ILogger _logger;
        public Neo4jMessageRepository(IOptions<Neo4jOptions> optionsAccessor,
                                      Microsoft.Extensions.Logging.ILogger<Neo4jMessageRepository> logger)
        {
            Options = optionsAccessor.Value;
            Logger = logger;
            Logger.LogInformation($"Neo4j Options: {Options.Uri} {Options.Name}:{Options.Password}");
            Driver = GraphDatabase.Driver(Options.Uri, AuthTokens.Basic(Options.Name, Options.Password));
        }

        public IDriver Driver { get => _driver; set => _driver = value; }
        public Neo4jOptions Options { get => _options; set => _options = value; }
        public Microsoft.Extensions.Logging.ILogger Logger { get => _logger; set => _logger = value; }

        public void Dispose()
        {
            Driver?.Dispose();
        }
        private string BuildPasswordChangeUrl()
        {
            var builder = new UriBuilder();
            builder.Scheme = "http";
            builder.Port = 7474;
            builder.Host = Options.Server;
            builder.Path = "/user/neo4j/password";
            return builder.Uri.AbsoluteUri;
        }

        public bool ChangePassword(string password)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(BuildPasswordChangeUrl());
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            httpWebRequest.Headers.Add(HttpRequestHeader.Authorization, "Basic bmVvNGo6bmVvNGo=");
            httpWebRequest.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            httpWebRequest.Headers.Add(HttpRequestHeader.Accept, "application/json;charset=UTF8");

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = $"{{\"password\":\"{password}\"}}";

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            return httpResponse.StatusCode == HttpStatusCode.OK ? true : false;
        }

        public string Ping(string message)
        {
          try
          {
            using (var session = _driver.Session())
            {
                var greeting = session.WriteTransaction(tx =>
                {
                    var result = tx.Run("CREATE (a:Greeting) " +
                                        "SET a.message = $message " +
                                        "RETURN a.message + ', from node ' + id(a)",
                        new {message});
                    return result.Single()[0].As<string>();
                });
                return greeting;
            }
          }
          catch(ClientException e)
          {
            Logger.LogError(new EventId(3), e, "Exception in Ping. Change Password?");
            Logger.LogInformation(BuildPasswordChangeUrl());
            using (var session = _driver.Session())
            {
                var result = session.Run($"CALL dbms.changePassword('root')");
                Logger.LogInformation(String.Join(",",result));
            }
            return "Change password";
          }
        }
    }

}
