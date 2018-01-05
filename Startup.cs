﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using TomatoAPI.Data;

namespace TomatoAPI
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
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // *********** CONFIGURES POSTRES FOR GIVEN CONTEXTS VIA DEP. INJ. *********
            ReadEnvVars();
            var connectionString = Globals.env["DOTNET_ENV"] == "Production" ? Globals.env["CONNECTION_STRING"] : Configuration.GetConnectionString("DatabaseUrl");

            services.AddEntityFrameworkNpgsql().AddDbContext<TomatoDbContext>(options => options.UseNpgsql(connectionString));
            // *********** CONFIGURES POSTRES FOR GIVEN CONTEXTS VIA DEP. INJ. *********

            services.AddMvc(); // Add framework services.
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // Makes the API open for any queries
            // !!!!!!!!! NOT SECURE !!!!!!!!
            // !!!!!!!!! NOT SECURE !!!!!!!!
            // !!!!!!!!! NOT SECURE !!!!!!!!
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials());

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }

        private void ReadEnvVars()
        {
            if (Environment.GetEnvironmentVariable("DOTNET_ENV") == "Production")
            {
                var enumerator = Environment.GetEnvironmentVariables().GetEnumerator();
                while (enumerator.MoveNext())
                {
                    // adds ALL env vars not only those passed by docker
                    Globals.env.Add(enumerator.Key.ToString(), enumerator.Value.ToString());
                }
            }
            else
            {
                string envFile = "./.env";
                if (System.IO.File.Exists(envFile)) {
                    string[] lines = System.IO.File.ReadAllLines(envFile);
                    foreach (string line in lines)
                    {
                        var index = line.IndexOf("=");
                        Globals.env.Add(line.Substring(0, index), line.Substring(index+1));
                    }
                }
                
                if (!Globals.env.ContainsKey("DOTNET_ENV"))
                {
                    Globals.env.Add("DOTNET_ENV", "Development");
                }
            }
        }
    }
}
