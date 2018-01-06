using System;
using System.Collections.Generic;

namespace DotNetAPI
{
    public static class Globals
    {
        public static Dictionary<string, string> env = new Dictionary<string, string>();
        private static bool haveRead = false;

        public static void ReadEnviromentVariables()
        {
            if (!haveRead) {
                if (System.Environment.GetEnvironmentVariable("DOTNET_ENV") == "Production") {
                    ReadEnviromentVariablesProduction();
                }
                else
                {
                    ReadEnviromentVariablesDevelopment("./.env");
                }
                haveRead = true;
            }
        }

        private static void ReadEnviromentVariablesProduction()
        {
          var enumerator = System.Environment.GetEnvironmentVariables().GetEnumerator();
          while (enumerator.MoveNext())
          {
              // adds ALL env vars not only those passed by docker
              Globals.env.Add(enumerator.Key.ToString(), enumerator.Value.ToString());
          }
        }

        private static void ReadEnviromentVariablesDevelopment(string path)
        {
            if (System.IO.File.Exists(path))
            {
                string[] lines = System.IO.File.ReadAllLines(path);
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
