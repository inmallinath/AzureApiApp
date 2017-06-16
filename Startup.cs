using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureApiApp.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace AzureApiApp
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            _env = env;
        }

        public IConfigurationRoot Configuration { get; }
        private IHostingEnvironment _env { get; }
        private string _connectionString = String.Empty;
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.

            if (_env.IsDevelopment())
            {
                _connectionString = Environment.GetEnvironmentVariable("PSQL_CONNECTION_STRING");
                services.AddDbContext<ApiDbContext>(options =>
                    options.UseNpgsql(_connectionString));
            }
            if(_env.IsProduction())
            {
                _connectionString = Environment.GetEnvironmentVariable("AZURE_CONNECTION_STRING");
                services.AddDbContext<ApiDbContext>(options =>
                                    options.UseNpgsql(_connectionString));
            }
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
        }
    }
}
