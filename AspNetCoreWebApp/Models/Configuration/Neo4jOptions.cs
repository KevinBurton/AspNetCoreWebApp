namespace AspNetCoreWebApp.Models.Configuration
{
  public class Neo4jOptions
  {

      public string Server { get; set; } = "neo4j";
      public string Uri { get; set; } = "bolt://neo4j:7687";
      public string Name { get; set; } = "neo4j";
      public string Password { get; set; } = "root";
   }
}
