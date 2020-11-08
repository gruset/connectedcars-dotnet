using System;
using System.Net;
using System.Net.Http;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using connectedcars_dotnet.Helpers;
using connectedcars_dotnet.Models;


namespace connectedcars_dotnet.Workers
{
    class ApiWorker
    {
        Log log = new Log();

        public AuthTokenModel GetAuth(Account account)
        {
            string result = ApiPost(account, "auth");
            var res = JsonConvert.DeserializeObject<AuthTokenModel>(result);

            return res;
        }

        public string GetVehicleOverview(Account account, string _token)
        {
            string result = ApiPost(account, "VehicleOverview", _token);
            return result;
        }

        public string GetVehicleTrips(Account account, string _token)
        {
            string result = ApiPost(account, "VehicleTrips", _token);
            return result;
        }

        private string ApiPost (Account account, string _function, string _token = "")
        {
            string result = "";

            var user = JObject.FromObject(new
            {
                email = account.Email,
                password = account.Password
            });

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
                    { "x-organization-namespace", account.Namespace }
                },
                Content = data
            };

            Queries q = new Queries(); // initiate a new instance of Queries to acces the predefined queries.

            var objVehicleOverview = JObject.FromObject(new
            {
                query = q.VehicleOverview()
            });

            var objVehicleTrips = JObject.FromObject(new
            {
                query = q.VehicleTrips()
            });

            var dataVehicleOverview = new StringContent(objVehicleOverview.ToString(), Encoding.UTF8, "application/json");
            var dataVehicleTrips = new StringContent(objVehicleTrips.ToString(), Encoding.UTF8, "application/json");

            var request_vehicleOverview = new HttpRequestMessage
            {
                RequestUri = new Uri(data_url),
                Method = HttpMethod.Post,
                Headers =
                {
                    { HttpRequestHeader.ContentType.ToString(), "application/json" },
                    { HttpRequestHeader.Accept.ToString(), "application/json" },
                    { "x-organization-namespace", account.Namespace },
                    { HttpRequestHeader.Authorization.ToString(), $"Bearer { _token }" }
                },
                Content = dataVehicleOverview
            };

            var request_vehicleTrips = new HttpRequestMessage
            {
                RequestUri = new Uri(data_url),
                Method = HttpMethod.Post,
                Headers =
                {
                    { HttpRequestHeader.ContentType.ToString(), "application/json" },
                    { HttpRequestHeader.Accept.ToString(), "application/json" },
                    { "x-organization-namespace", account.Namespace },
                    { HttpRequestHeader.Authorization.ToString(), $"Bearer { _token }" }
                },
                Content = dataVehicleTrips
            };

            if (_function == "auth")
            {
                HttpResponseMessage response = client.SendAsync(request_auth).Result;
                result = response.Content.ReadAsStringAsync().Result;
            }
            else if (_function == "VehicleOverview")
            {
                HttpResponseMessage response = client.SendAsync(request_vehicleOverview).Result;
                result = response.Content.ReadAsStringAsync().Result;
            }
            else if (_function == "VehicleTrips")
            {
                HttpResponseMessage response = client.SendAsync(request_vehicleTrips).Result;
                result = response.Content.ReadAsStringAsync().Result;
            }

            return result;
        }
    }
}
