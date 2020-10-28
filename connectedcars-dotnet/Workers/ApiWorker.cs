using System;
using System.Collections.Generic;
using System.Text;

using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using connectedcars_dotnet.Helpers;
using connectedcars_dotnet.Models;
using System.Net.Http.Headers;
using System.Net;

namespace connectedcars_dotnet.Workers
{
    class ApiWorker
    {
        private ConfigModel config = new Config().Configuration();
        Log log = new Log();

        public string GetAuth()
        {
            var res = ApiPost(config.Email, config.Password, config.Namespace);
            return res;
        }

        private string ApiPost (string _email, string _password, string _namespace)
        {
            var user = new User();
            user.email = _email;
            user.password = _password;

            var json = JsonConvert.SerializeObject(user);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var auth_url = "https://auth-api.connectedcars.io/auth/login/email/password";

            using var client = new HttpClient();

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(auth_url),
                Method = HttpMethod.Post,
                Headers =
                {
                    { HttpRequestHeader.ContentType.ToString(), "application/json" },
                    { HttpRequestHeader.Accept.ToString(), "application/json" },
                    { "x-organization-namespace", _namespace }
                },
                Content = data
            };

            //log.LogToConsole(request.Content.ReadAsStringAsync().Result);

            HttpResponseMessage response = client.SendAsync(request).Result;
            string result = response.Content.ReadAsStringAsync().Result;
            
            //var response = await client.PostAsync(auth_url, data);
            //string result = response.Content.ReadAsStringAsync().Result;

            return result;
        }
        private class User
        {
            public string email { get; set; }
            public string password { get; set; }
        }
    }
}
