namespace AspNetCoreWebApp.Models.Configuration
{
  public class MongoOptions
  {
      public string Uri { get; set; } = "mongodb://mongo/?safe=true";
      public string Database { get; set; } = "user";
      public string Collection { get; set; } = "users";

  }
}
