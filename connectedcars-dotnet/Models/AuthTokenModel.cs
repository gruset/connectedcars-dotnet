using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace connectedcars_dotnet.Models
{
    public class AuthTokenModel
    {
        [JsonProperty("oldToken")]
        public string OldToken { get; set; }
        [JsonProperty("Token")]
        public string Token { get; set; }
        [JsonProperty("level")]
        public string Level { get; set; }
        [JsonProperty("expires")]
        public int Expires { get; set; }
    }
}
