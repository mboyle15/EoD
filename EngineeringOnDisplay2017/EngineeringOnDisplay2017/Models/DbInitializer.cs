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
            BuildingRecord testBuilding = null;


            if (!context.BuildingRecords.Any())
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

                context.SaveChanges();
            }


            testBuilding = context.BuildingRecords.Where(record => record.Acronym == "EIB").FirstOrDefault();

            if (!context.ElectricalRecords.Any())
            {
                DateTime recordTime = DateTime.Now.AddMinutes(-1000*15);
                float usage = 296797.94f;
                float demand = 9.08f;
                float currentDemand;
                Random randomNum = new Random();

                for (int i = 0; i < 1000; i++)
                {
                    currentDemand = demand + ((float)randomNum.Next(-300, 300)) / 100f; //add or subtract a random amount from the demand to get current demand.  Wanted to have three points of precition so why it goes 
                    usage = usage + currentDemand / 4f; //calculate how much useage for 15 minutes based on the current demand

                    context.Add(new ElectricalRecord { RecordedDateTime = recordTime, Amount = usage, Change = currentDemand, Building = testBuilding });
                    recordTime = recordTime.AddMinutes(15);
                }
            }

            if (!context.WaterRecords.Any())
            {
                DateTime recordTime = DateTime.Now.AddMinutes(-1000 * 15);
                float usage = 1065.0f;
                float demand = 1.0f;
                float currentDemand;
                Random randomNum = new Random();

                for (int i = 0; i < 1000; i++)
                {
                    currentDemand = demand + ((float)randomNum.Next(-95, 200)) / 100f; //add or subtract a random amount from the demand to get current demand.  Wanted to have three points of precition so why it goes 
                    usage = usage + currentDemand / 4f; //calculate how much useage for 15 minutes based on the current demand

                    context.Add(new WaterRecord { RecordedDateTime = recordTime, Amount = usage, Building = testBuilding });
                    recordTime = recordTime.AddMinutes(15);
                }
            }

            if (!context.NaturalGasRecords.Any())
            {
                DateTime recordTime = DateTime.Now.AddMinutes(-1000 * 15);
                float usage = 53314.0f;
                float demand = 1.0f;
                float currentDemand;
                Random randomNum = new Random();

                for (int i = 0; i < 1000; i++)
                {
                    currentDemand = demand + ((float)randomNum.Next(-50, 100)) / 100f; //add or subtract a random amount from the demand to get current demand.  Wanted to have three points of precition so why it goes 
                    usage = usage + currentDemand / 4f; //calculate how much useage for 15 minutes based on the current demand

                    context.Add(new NaturalGasRecord { RecordedDateTime = recordTime, Amount = usage, Building = testBuilding });
                    recordTime = recordTime.AddMinutes(15);
                }
            }

            if (!context.OutsideTempRecords.Any())
            {
                DateTime recordTime = DateTime.Now.AddMinutes(-1000 * 15);
                float temperature = 60.0f;
                Random randomNum = new Random();

                for (int i = 0; i < 1000; i++)
                {

                    if(temperature > 80f)
                    {
                        temperature = temperature + randomNum.Next(-5, 0);
                    }
                    else if(temperature < 20f)
                    {
                        temperature = temperature + randomNum.Next(0, 5);
                    }
                    else
                    {
                        temperature = temperature + randomNum.Next(-5, 5);
                    }

                    context.Add(new OutsideTempRecord { RecordedDateTime = recordTime, Amount = temperature, Building = testBuilding });
                    recordTime = recordTime.AddMinutes(15);
                }

            }
            context.SaveChanges();
        }
    }
}
