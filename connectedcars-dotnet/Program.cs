using System;

using System.Collections;
using System.Linq;
using connectedcars_dotnet.Helpers;
using connectedcars_dotnet.Workers;
using connectedcars_dotnet.Models;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace connectedcars_dotnet
{
    class Program
    {
        static Log log = new Log();
        static ApiWorker apiWrk = new ApiWorker();
        static ConfigModel config = new Config().Configuration();

        static void Main(string[] args)
        {
            log.LogToConsole("Hello World!");

            foreach (var account in config.Accounts)
            {
                var AuthJson = apiWrk.GetAuth(account);
                GetVehicles(AuthJson, account);
            }

            GetVehicleTrips(1); // Use the Id vaule set in config.json for the selected account --> this is a manual input value!
        }

        static void GetVehicleTrips(int AccountId)
        {
            var account = config.Accounts.Where(a => a.Id == AccountId).First();
            var AuthJson = apiWrk.GetAuth(account);

            var VehicleTrips = apiWrk.GetVehicleTrips(account, AuthJson.Token);

            JObject vTrips = JObject.Parse(VehicleTrips);
            var vehicles = vTrips["data"]["viewer"]["vehicles"][0]["vehicle"];  // Currently there is only one vehicle per account, hence [0]
            var trips = JObject.Parse(vehicles.ToString());
            IList tripslist = trips["trips"]["items"].Children().ToList();

            log.LogToConsole("");
            log.LogToConsole(String.Format("|    {0,-21}|    {1,-15}|    {2,-15}|", "Time", "Mileage (km)", "TripType"));

            foreach (var t in tripslist)
            {
                var trip = JObject.Parse(t.ToString());
                string id = trip["id"].ToString();
                string duration = trip["duration"].ToString();
                string mileage = trip["mileage"].ToString();
                string odometerMileage = trip["odometerMileage"].ToString();
                string gpsMileage = trip["gpsMileage"].ToString();
                string tripType = trip["tripType"].ToString();                  // If marked as "business" in the app it outputs "business" otherwise it is empty
                string time = trip["time"].ToString();

                log.LogToConsole(String.Format("|{0,-25}|    {1,-15}|    {2,-15}|", time, mileage, tripType));
            }
        }

        static void GetVehicles(AuthTokenModel _auth, Account _account)
        {
            var VehicleOverview = apiWrk.GetVehicleOverview(_account, _auth.Token);
            JObject ObjVehicles = JObject.Parse(VehicleOverview);
            IList Vehicles = ObjVehicles["data"]["viewer"]["vehicles"].Children().ToList();

            foreach (var Vehicle in Vehicles)
            {
                JObject v = JObject.Parse(Vehicle.ToString());
                string id = v["vehicle"]["id"].ToString();
                string make = v["vehicle"]["make"].ToString();
                string model = v["vehicle"]["model"].ToString();
                string name = v["vehicle"]["name"].ToString();
                int odometer = (int)v["vehicle"]["odometer"]["odometer"];
                string fuelLevel = v["vehicle"]["fuelLevel"]["liter"].ToString();
                string latitude = v["vehicle"]["position"]["latitude"].ToString();
                string longitude = v["vehicle"]["position"]["longitude"].ToString();

                log.LogToConsole("");
                log.LogToConsole("Id:\t\t" + id);
                log.LogToConsole("Make:\t\t" + make);
                log.LogToConsole("Model:\t\t" + model);
                log.LogToConsole("Name:\t\t" + name);
                log.LogToConsole("Odometer:\t" + odometer.ToString("#,##") + " km");
                log.LogToConsole("Fuel level:\t" + fuelLevel + " l");
                log.LogToConsole("-- Position --");
                log.LogToConsole("Latitude:\t" + latitude);
                log.LogToConsole("Longitude:\t" + longitude);
                log.LogToConsole("Google Maps:\t" + "http://maps.google.com/maps?q=" + latitude + "," + longitude);
            }


        }
    }
}