using connectedcars_dotnet.Helpers;
using connectedcars_dotnet.Workers;
using System;

namespace connectedcars_dotnet
{
    class Program
    {
        static Log log = new Log();
        static ApiWorker apiWrk = new ApiWorker();

        static void Main(string[] args)
        {
            log.LogToConsole("Hello World!");

            var AuthJson = apiWrk.GetAuth();
            //log.LogToConsole(AuthJson.token);
            var VehicleOverview = apiWrk.GetVehicleOverview(AuthJson.token);
            log.LogToConsole(VehicleOverview);
        }
    }
}
