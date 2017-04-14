﻿/****************************************************************************************************************
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
 * File: OutsideTempRepository.cs
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
using System.Linq;
using System.Threading.Tasks;

namespace EngineeringOnDisplay2017.Models
{
    public class OutsideTempRepository : IOutsideTempRepository
    {
        //local copy of context passed in by dependency injection
        private AppDbContext _appDbContext;

        //simple constructor that stores the local reference to appDbContext passed in though dependency injection
        //  and does a hard code set of 
        public OutsideTempRepository(AppDbContext appDbContext)
        {

            //store the Context for the database.  Note Context is the glue to the database.  All calls for database go through AppDbContext for this case.  
            _appDbContext = appDbContext;


            //hard set the building to the EIB.  Will have to look how to program for this in the future. 
            CurrentBuilding = _appDbContext.BuildingRecords.FirstOrDefault(record => record.Acronym == "EIB");
        }

        //property for storing the crrent building to be used in future queries.
        public BuildingRecord CurrentBuilding { get; set; }

        //return all records with or without specifing the building
        public IEnumerable<OutsideTempRecord> GetOutsideTempRecords()
        {
            //if no building specified
            if (CurrentBuilding == null)
            {
                //return the entire table of records
                return _appDbContext.OutsideTempRecords.ToList();
            }

            //else return list of records with currentbuilding specified
            return _appDbContext.OutsideTempRecords.Where(record => record.BuildingRecord == CurrentBuilding).ToList();
        }

        //get all records in timeframe with or without concer for the current building
        public IEnumerable<OutsideTempRecord> GetOutsideTempRecords(DateTime start, DateTime end)
        {
            //return a list of records within or equal to the timeframe without concern for current building.
            if (CurrentBuilding == null)
            {
                return _appDbContext.OutsideTempRecords.Where(record => record.RecordedDateTime >= start && record.RecordedDateTime <= end).ToList();
            }

            //else return a list of records within or equal to the timeframe with concern for current building.
            return _appDbContext.OutsideTempRecords.Where(record => record.RecordedDateTime >= start && record.RecordedDateTime <= end && record.BuildingRecord == CurrentBuilding).ToList();
        }

        //get a record that matches the passed in record id.
        public OutsideTempRecord GetOutsideTempRecordById(int recordId)
        {
            return _appDbContext.OutsideTempRecords.FirstOrDefault(record => record.OutsideTempRecordId == recordId);
        }
    }
}