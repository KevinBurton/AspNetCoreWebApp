using System;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using AspNetCoreWebApp.Models;
using AspNetCoreWebApp.Models.Implementations;

namespace AspNetCoreWebApp.Database
{
    public interface IMongoDbContext
    {
        DbSet<User<ObjectId>> Users { get; set; }
        DbSet<Crime<ObjectId>> Crime { get; set; }
    }
}
