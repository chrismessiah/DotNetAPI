using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace TomatoAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ReadEnvVars();

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

        private static void ReadEnvVars()
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
