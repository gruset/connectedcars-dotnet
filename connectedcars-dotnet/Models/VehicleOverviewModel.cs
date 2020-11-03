using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace connectedcars_dotnet.Models
{
    public class VehicleOverviewModel
    {
        [JsonProperty("data")]
        public Data Data { get; set; }
    }

    public class Health
    {
        [JsonProperty("ok")]
        public bool Ok { get; set; }

        [JsonProperty("recommendation")]
        public object Recommendation { get; set; }
    }

    public class Ignition
    {
        [JsonProperty("time")]
        public DateTime Time { get; set; }

        [JsonProperty("on")]
        public bool On { get; set; }
    }

    public class OdometerInfo
    {
        [JsonProperty("time")]
        public DateTime Time { get; set; }

        [JsonProperty("odometer")]
        public int Odometer { get; set; }
    }

    public class FuelLevel
    {
        [JsonProperty("time")]
        public DateTime Time { get; set; }

        [JsonProperty("liter")]
        public int Liter { get; set; }
    }

    public class Position
    {
        [JsonProperty("time")]
        public DateTime Time { get; set; }

        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }
    }

    public class Vehicle2
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("licensePlate")]
        public string LicensePlate { get; set; }

        [JsonProperty("unitSerial")]
        public string UnitSerial { get; set; }

        [JsonProperty("make")]
        public string Make { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("fuelEconomy")]
        public double FuelEconomy { get; set; }

        [JsonProperty("health")]
        public Health Health { get; set; }

        [JsonProperty("ignition")]
        public Ignition Ignition { get; set; }

        [JsonProperty("odometer")]
        public OdometerInfo Odometer { get; set; }

        [JsonProperty("fuelLevel")]
        public FuelLevel FuelLevel { get; set; }

        [JsonProperty("position")]
        public Position Position { get; set; }
    }

    public class VehicleInfo
    {
        [JsonProperty("vehicle")]
        public Vehicle2 Vehicle { get; set; }
    }

    public class Viewer
    {
        [JsonProperty("vehicles")]
        public List<VehicleInfo> Vehicles { get; set; }
    }

    public class Data
    {
        [JsonProperty("viewer")]
        public Viewer Viewer { get; set; }
    }
}
