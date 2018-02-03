using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

using AspNetCoreWebApp.Database;
using AspNetCoreWebApp.Models.Configuration;
using AspNetCoreWebApp.Repositories.Interfaces;
using AspNetCoreWebApp.Data.Repositories.Implmentations;

namespace AspNetCoreWebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MongoDbContext>(options => options.UseInMemoryDatabase("name"));
            services.AddMvc();
            services.AddOptions();
            services.Configure<Neo4jOptions>(Configuration.GetSection("Neo4j"));
            services.Configure<MongoOptions>(Configuration.GetSection("Mongo"));
            services.Configure<RedisOptions>(Configuration.GetSection("Redis"));
            services.AddScoped<ICacheRepository,RedisRepository>();
            services.AddScoped(typeof(IDocumentRepository<>),typeof(MongoRepository<>));
            services.AddScoped<IGraphRepository, Neo4jRepository>();
            services.AddScoped<IMongoDbContext, MongoDbContext>();
       }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
