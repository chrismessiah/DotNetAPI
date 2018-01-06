using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace DotNetAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Globals.ReadEnviromentVariables();
            string hostingUrl = (Globals.env["DOTNET_ENV"] == "Production") ? "http://0.0.0.0:5000" : "http://localhost:5000";

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseUrls(hostingUrl)
                .Build();
            host.Run();
        }
    }
}
