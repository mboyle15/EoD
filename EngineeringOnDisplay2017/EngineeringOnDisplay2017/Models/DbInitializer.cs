using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EngineeringOnDisplay2017.Models
{
    public class DbInitializer
    {

        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            AppDbContext context = applicationBuilder.ApplicationServices.GetRequiredService<AppDbContext>();
            BuildingRecord testBuilding =null;

            if(!context.BuildingRecords.Any())
            {
                testBuilding = new BuildingRecord
                {
                    Name = "Engineering and Industry Building",
                    Acronym = "EIB",
                    AddressLineOne = "123 Sesseme Street",
                    City = "Anchorage",
                    State = "AK",
                    Zip = "99502"
                };

                context.Add(testBuilding);
            }

            if(!context.EletricalRecords.Any())
            {

                context.AddRange
                (

                    new ElectricalRecord { RecordedDateTime = DateTime.Now.AddMinutes(-120), Usage = 296637.94f, Demand = 8.08f, BuildingRecord = testBuilding },
                    new ElectricalRecord { RecordedDateTime = DateTime.Now.AddMinutes(-105), Usage = 296657.94f, Demand = 7.08f, BuildingRecord = testBuilding },
                    new ElectricalRecord { RecordedDateTime = DateTime.Now.AddMinutes(-90), Usage = 296677.94f, Demand = 9.08f, BuildingRecord = testBuilding },
                    new ElectricalRecord { RecordedDateTime = DateTime.Now.AddMinutes(-75), Usage = 296687.94f, Demand = 10.08f, BuildingRecord = testBuilding },
                    new ElectricalRecord { RecordedDateTime = DateTime.Now.AddMinutes(-60), Usage = 296697.94f, Demand = 11.08f, BuildingRecord = testBuilding },
                    new ElectricalRecord { RecordedDateTime = DateTime.Now.AddMinutes(-45), Usage = 296717.94f, Demand = 7.08f, BuildingRecord = testBuilding },
                    new ElectricalRecord { RecordedDateTime = DateTime.Now.AddMinutes(-30), Usage = 296737.94f, Demand = 2.08f, BuildingRecord = testBuilding },
                    new ElectricalRecord { RecordedDateTime = DateTime.Now.AddMinutes(-15), Usage = 296787.94f, Demand = 5.08f, BuildingRecord = testBuilding },
                    new ElectricalRecord { RecordedDateTime = DateTime.Now, Usage = 296797.94f, Demand = 9.08f, BuildingRecord = testBuilding }
                );
            }

            context.SaveChanges();
        }
    }
}
