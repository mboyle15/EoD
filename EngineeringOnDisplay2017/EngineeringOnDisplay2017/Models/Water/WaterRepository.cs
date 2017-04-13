/****************************************************************************************************************
 * Project: Engineering on Display
 * Purpose: Website for displaying sensor data from the Engineering and Industry Building at the University of
 *          Alaska, Anchorage.  First features is a front end website for the general users to view
 *          the graphical data of the sensors and read more on the purpose of the display.  Second Feature
 *          is a admin secured website to add more slideshows to the third feature and view statistics. The third feature
 *          is a slide show of different advertisements.  
 * 
 * Authors:  Martin Boyle
 *           Terrance Mount
 *           Andrew Smart
 *           
 * Sponsor: Dr. Kenrick Mock
 * 
 * Instructor: Dr. Martin Cenek
 * 
 * Class:  CSCE 470 Capstone  Spring 2017
 * College: University of Alaska, Anchorage
 * ***********************************************************************************************************************
 * File: WaterRepository.cs
 * Purpose: Interact with AppDbContext to retrieve and proccess Linq method like calls for SQL database.  
 *          Needed to use depencency injection for controllers and views.  Must have a corrosponding Interface to allow
 *          Controllers to access methods in this class.  
 *      
 *Linq methods are how Enity Framework formats its SQL queries.  Note each of the method strings have to end with
 *      ether .FirstOrDefault to get one row or .ToList to get a IEnumerable<> list of the rows.
 *      
 * Used Linq methods and purpose:     
 *      .Where(lamda expression)  this modifies the return like a WHERE in SQL
 *              -Example .Where(e => e.RecordedTime > DateTime.Now.AddMinutes(-60)) This will return all matching row 
 *              where the recorded time is within the last hour.  
 *              
 *      .FirstOrDefault(lamda expression)  will return one instance of the tables model ie ElectricalRecord.cs
 *              -example .FirstOrDefault(e => e.ElectricalRecordId == 1) will return a instance of ElectricalRecord with
 *              Id equal to 1 if it exists.  
 *              
 *      .ToList() Return all matching rows in an IEnumerable<> list
 *      
 *      .AddRange() used in adding records/rows to the database
 * *******************************************************************************************************************/

using System;
using System.Collections.Generic;

namespace EngineeringOnDisplay2017.Models
{
    public class WaterRepository : IWaterRepository
    {
        public BuildingRecord CurrentBuilding { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public WaterRecord GetWaterRecordById(int eletricalRecordId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WaterRecord> GetWaterRecords()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WaterRecord> GetWaterRecords(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
