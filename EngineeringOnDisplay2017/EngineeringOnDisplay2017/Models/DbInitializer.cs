using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace EngineeringOnDisplay2017.Models
{
    public class DbInitializer
    {

        private AppDbContext _context;
        private UserManager<IdentityUser> _userManager;

        public DbInitializer(AppDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            
        }


        public async Task EnsureSeedData()
        {
            if(await _userManager.FindByNameAsync("mounter") == null)
            {
                var user = new IdentityUser()
                {
                    UserName = "mounter",
                    Email = "mounter@eod.edu"
                };

                await _userManager.CreateAsync(user, "P@ssw0rd!");
            }

            if(!_context.Slides.Any())
            {
                var slides = new List<Slide>()
                {
                    new Slide(){ FullUrl="~/images/passiveAdsImages/test1.jpg", ThumbUrl="~/images/passiveAdsImages/thumbnails/tb-test1.jpg", Order=0, TimeSeconds=2},
                    new Slide(){ FullUrl="~/images/passiveAdsImages/test2.jpg", ThumbUrl="~/images/passiveAdsImages/thumbnails/tb-test2.jpg", Order=1, TimeSeconds=3},
                    new Slide(){ FullUrl="~/images/passiveAdsImages/test3.jpg", ThumbUrl="~/images/passiveAdsImages/thumbnails/tb-test3.jpg", Order=2, TimeSeconds=2},
                    new Slide(){ FullUrl="~/images/passiveAdsImages/test4.jpg", ThumbUrl="~/images/passiveAdsImages/thumbnails/tb-test4.jpg", Order=3, TimeSeconds=4},
                    new Slide(){ FullUrl="~/images/passiveAdsImages/test5.jpg", ThumbUrl="~/images/passiveAdsImages/thumbnails/tb-test5.jpg", Order=4, TimeSeconds=5},
           
                };
                _context.AddRange(slides);
            }

            BuildingRecord testBuilding = null;
            long numRecordsToGenerate = 175200;

            if (!_context.BuildingRecords.Any())
            {
                testBuilding = new BuildingRecord
                {
                    Name = "Engineering and Industry Building",
                    Acronym = "EIB",
                    AddressLineOne = "2900 Spirit Dr",
                    City = "Anchorage",
                    State = "AK",
                    Zip = "99508"
                };

                _context.Add(testBuilding);

               await _context.SaveChangesAsync();
            }


            testBuilding = _context.BuildingRecords.Where(record => record.Acronym == "EIB").FirstOrDefault();

            if (!_context.ElectricalRecords.Any())
            {
                DateTime recordTime = DateTime.Now.AddMinutes(numRecordsToGenerate * -15);
                float usage = 296797.94f;
                float demand = 9.08f;
                float currentDemand;
                Random randomNum = new Random();

                for (int i = 0; i < numRecordsToGenerate; i++)
                {
                    currentDemand = demand + ((float)randomNum.Next(-300, 300)) / 100f; //add or subtract a random amount from the demand to get current demand.  Wanted to have three points of precition so why it goes 
                    usage = usage + currentDemand / 4f; //calculate how much useage for 15 minutes based on the current demand

                    _context.Add(new ElectricalRecord { RecordedDateTime = recordTime, Amount = usage, Change = currentDemand, Building = testBuilding });
                    recordTime = recordTime.AddMinutes(15);
                }
            }

            if (!_context.WaterRecords.Any())
            {
                DateTime recordTime = DateTime.Now.AddMinutes(-(numRecordsToGenerate) * 15);
                float usage = 1065.0f;
                float demand = 1.0f;
                float currentDemand;
                Random randomNum = new Random();

                for (int i = 0; i < numRecordsToGenerate; i++)
                {
                    currentDemand = demand + ((float)randomNum.Next(-95, 200)) / 100f; //add or subtract a random amount from the demand to get current demand.  Wanted to have three points of precition so why it goes 
                    usage = usage + currentDemand / 4f; //calculate how much useage for 15 minutes based on the current demand

                    _context.Add(new WaterRecord { RecordedDateTime = recordTime, Amount = usage, Building = testBuilding });
                    recordTime = recordTime.AddMinutes(15);
                }
            }

            if (!_context.NaturalGasRecords.Any())
            {
                DateTime recordTime = DateTime.Now.AddMinutes(-(numRecordsToGenerate) * 15);
                float usage = 53314.0f;
                float demand = 1.0f;
                float currentDemand;
                Random randomNum = new Random();

                for (int i = 0; i < numRecordsToGenerate; i++)
                {
                    currentDemand = demand + ((float)randomNum.Next(-50, 100)) / 100f; //add or subtract a random amount from the demand to get current demand.  Wanted to have three points of precition so why it goes 
                    usage = usage + currentDemand / 4f; //calculate how much useage for 15 minutes based on the current demand

                    _context.Add(new NaturalGasRecord { RecordedDateTime = recordTime, Amount = usage, Building = testBuilding });
                    recordTime = recordTime.AddMinutes(15);
                }
            }

            if (!_context.OutsideTempRecords.Any())
            {
                DateTime recordTime = DateTime.Now.AddMinutes(-(numRecordsToGenerate) * 15);
                float temperature = 60.0f;
                Random randomNum = new Random();

                for (int i = 0; i < numRecordsToGenerate; i++)
                {

                    if (temperature > 80f)
                    {
                        temperature = temperature + randomNum.Next(-5, 0);
                    }
                    else if (temperature < 20f)
                    {
                        temperature = temperature + randomNum.Next(0, 5);
                    }
                    else
                    {
                        temperature = temperature + randomNum.Next(-5, 5);
                    }

                    _context.Add(new OutsideTempRecord { RecordedDateTime = recordTime, Amount = temperature, Building = testBuilding });
                    recordTime = recordTime.AddMinutes(15);
                }

            }
           await _context.SaveChangesAsync();
        }
    }
}
