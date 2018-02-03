using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using AspNetCoreWebApp.Models;
using AspNetCoreWebApp.Models.Implementations;

namespace AspNetCoreWebApp.Database
{
    public class MongoDbContext : DbContext, IMongoDbContext
    {
        public MongoDbContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<User<ObjectId>> Users { get; set; }
        public DbSet<Crime<ObjectId>> Crime { get; set; }
    }
}