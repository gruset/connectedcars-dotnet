using System;
namespace connectedcars_dotnet.Models
{
    public class Queries
    {
        public string VehicleOverview()
        {
            return
                @"query User { 
                    viewer { 
                      vehicles { 
                        vehicle { 
                          id 
                          licensePlate
                          unitSerial 
                          make
                          model 
                          name
                          fuelEconomy 
                          health { 
                            ok
                            recommendation
                          } 
                          ignition { 
                            time 
                            on
                          } 
                          odometer { 
                            time 
                            odometer
                          } 
                          fuelLevel { 
                            time 
                            liter
                          } 
                          position { 
                            time 
                            latitude
                            longitude
                          } 
                        } 
                      } 
                    }
                 }";
        }
        public string VehicleTrips()
        {
            // input value for "trip" defines how many trips are received
            // --> (last:1000) retreives the last 1000 records
            // --> (first:1000) retreives the first 1000 records
            // adapt this to your need.
            return
                @"query User { 
                    viewer { 
                      vehicles { 
                        vehicle { 
                          id 
                          trips (last:1000){ 
                            items {
                              id
                              time
                              duration
                              mileage
                              odometerMileage
                              gpsMileage
                              tripType
                            }
                          }
                        } 
                      } 
                    }
                 }";
        }
    }
}
