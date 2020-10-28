using System;
using System.Collections.Generic;
using System.Text;

namespace connectedcars_dotnet.Models
{
    public class AuthTokenModel
    {
        public string oldToken { get; set; }
        public string token { get; set; }
        public string level { get; set; }
        public int expires { get; set; }
    }
}
