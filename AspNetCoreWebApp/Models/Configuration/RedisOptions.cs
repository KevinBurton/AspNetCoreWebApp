namespace AspNetCoreWebApp.Models.Configuration
{
  public class RedisOptions
  {
      public string Server { get; set; } = "redis";
      public int Port { get; set; } = 6379;
  }
}
