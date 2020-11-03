using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using connectedcars_dotnet.Models;
using Newtonsoft.Json;

namespace connectedcars_dotnet.Helpers
{
    public class Config
    {
        public ConfigModel Configuration()
        {
            String jsonConfig = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"config.json");
            return JsonConvert.DeserializeObject<ConfigModel>(jsonConfig);
        }
    }
}
