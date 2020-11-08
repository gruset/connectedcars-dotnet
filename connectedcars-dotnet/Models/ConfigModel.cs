using System;
using System.Collections.Generic;
using System.Text;

namespace connectedcars_dotnet.Models
{
    public class ConfigModel
    {
        public List<Account> Accounts { get; set; }
    }

    public class Account
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Namespace { get; set; }
        public int Id { get; set; }
    }
}
