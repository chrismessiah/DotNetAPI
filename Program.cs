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
            Console.WriteLine("111111");
            Globals.ReadEnviromentVariables();

            var foo = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>();

            if (Globals.env["DOTNET_ENV"] == "Production")
            {
                foo = foo.UseUrls("http://0.0.0.0:5000");
            }

            var host = foo .Build();
            host.Run();
        }
    }
}
