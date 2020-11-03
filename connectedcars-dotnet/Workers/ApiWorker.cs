using System;
using System.Collections.Generic;
using System.Text;

using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using connectedcars_dotnet.Helpers;
using connectedcars_dotnet.Models;
using System.Net.Http.Headers;
using System.Net;
using System.IO;


namespace connectedcars_dotnet.Workers
{
    class ApiWorker
    {
        private ConfigModel config = new Config().Configuration();
        Log log = new Log();

        public AuthTokenModel GetAuth()
        {
            string result = ApiPost(config.Email, config.Password, config.Namespace, "auth");
            var res = JsonConvert.DeserializeObject<AuthTokenModel>(result);

            return res;
        }

        public string GetVehicleOverview(string _token)
        {
            string result = ApiPost(config.Email, config.Password, config.Namespace, "VehicleOverview", _token);
            return result;
        }

        private string ApiPost (string _email, string _password, string _namespace, string _function, string _token = "")
        {
            string result = "";

            var user = new User();
            user.email = _email;
            user.password = _password;

            var json = JsonConvert.SerializeObject(user);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var auth_url = "https://auth-api.connectedcars.io/auth/login/email/password";
            var data_url = "https://api.connectedcars.io/graphql";

            using var client = new HttpClient();

            var request_auth = new HttpRequestMessage
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

            string queryVehicleOverview = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"Models/connectedcars-VehicleOverview.txt"); 

            var objVehicleOverview = JObject.FromObject(new
            {
                query = queryVehicleOverview
            });

            var dataVehicleOverview = new StringContent(objVehicleOverview.ToString(), Encoding.UTF8, "application/json");

            var request_data = new HttpRequestMessage
            {
                RequestUri = new Uri(data_url),
                Method = HttpMethod.Post,
                Headers =
                {
                    { HttpRequestHeader.ContentType.ToString(), "application/json" },
                    { HttpRequestHeader.Accept.ToString(), "application/json" },
                    { "x-organization-namespace", _namespace },
                    { HttpRequestHeader.Authorization.ToString(), $"Bearer { _token }" }
                },
                Content = dataVehicleOverview
            };

            //log.LogToConsole(request.Content.ReadAsStringAsync().Result);
            if (_function == "auth")
            {
                HttpResponseMessage response = client.SendAsync(request_auth).Result;
                result = response.Content.ReadAsStringAsync().Result;
            }
            else if (_function == "VehicleOverview")
            {
                HttpResponseMessage response = client.SendAsync(request_data).Result;
                result = response.Content.ReadAsStringAsync().Result;
            }
            
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
