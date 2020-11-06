using System;

using connectedcars_dotnet.Helpers;
using connectedcars_dotnet.Workers;
using connectedcars_dotnet.Models;

using Newtonsoft.Json;

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
                var VehicleOverview = apiWrk.GetVehicleOverview(account, AuthJson.Token);
                var voModel = JsonConvert.DeserializeObject<VehicleOverviewModel>(VehicleOverview);

                foreach (var v in voModel.Data.Viewer.Vehicles)
                {
                    log.LogToConsole("");
                    log.LogToConsole("Make:\t\t" + v.Vehicle.Make);
                    log.LogToConsole("Model:\t\t" + v.Vehicle.Model);
                    log.LogToConsole("Name:\t\t" + v.Vehicle.Name);
                    log.LogToConsole($"Odometer:\t{ v.Vehicle.Odometer.Odometer.ToString("#,##") } km");
                    log.LogToConsole($"Fuel level:\t{ v.Vehicle.FuelLevel.Liter.ToString()} l");
                    log.LogToConsole("Position:");
                    log.LogToConsole("--Latitude:\t" + v.Vehicle.Position.Latitude.ToString());
                    log.LogToConsole("--Longitude:\t" + v.Vehicle.Position.Longitude.ToString());
                    log.LogToConsole($"--Google Maps:\thttp://maps.google.com/maps?q={ v.Vehicle.Position.Latitude },{ v.Vehicle.Position.Longitude }");
                }
            }
        }
    }
}
