using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using AspNetCoreWebApp.Models.Implementations;

namespace AspNetCoreWebApp.Database
{
    public class MongoDbContext : DbContext
    {
        public MongoDbContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<User<ObjectId>> Users { get; set;  }
    }
}