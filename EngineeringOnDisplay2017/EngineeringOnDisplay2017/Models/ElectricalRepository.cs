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
 * File: ElectricalRecord.cs
 * Purpose: Model for the electrical sensor in buildings
 * 
 * Puedo-Code:
 *      properties for:
 *      
 *          Record Id - primary key
 *          Creation Time - the date and time this entry was created.  Most likely just the insertion date.  
 *          Usage - the kilowatthours in the csv file.  Looks like it will just continue to grow.  
 *          demand - the current power demand of the building in kilowatts
 *          Building Id - foriegn key for the building id tied to the record.
 * 
 * *******************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;

namespace EngineeringOnDisplay2017.Models
{
    public class ElectricalRepository : IElectricalRepository
    {
        //local copy of context passed in by dependency injection
        private AppDbContext _appDbContext;

        //simple constructor that stores the local reference to appDbContext passed in though dependency injection
        //  and does a hard code set of 
        public ElectricalRepository(AppDbContext appDbContext)
        {

            //store the Context for the database.  Note Context is the glue to the database.  All calls for database go through AppDbContext for this case.  
            _appDbContext = appDbContext;


            //hard set the building to the EIB.  Will have to look how to program for this in the future. 
            CurrentBuilding = _appDbContext.BuildingRecords.FirstOrDefault(b => b.Acronym == "EIB");
        }

        //property to contain which building is currently being looked at
        public BuildingRecord CurrentBuilding { get; set; }
  

        //get a Electrical record that matches the passed in electrical record id.
        public ElectricalRecord GetElectricalRecordById(int eletricalRecordId)
        {
            return _appDbContext.EletricalRecords.FirstOrDefault(e => e.ElectricalRecordId == eletricalRecordId);
        }

        //get all records in the eletrical table with or without specifing a building.
        public IEnumerable<ElectricalRecord> GetElectricalRecords()
        {
            //if no building specified
            if(CurrentBuilding == null)
            {
                //return the entire table of electrical records
                return _appDbContext.EletricalRecords.ToList();
            }

            //else return list of electrical records with currentbuilding specified
            return _appDbContext.EletricalRecords.Where(e => e.BuildingRecord == CurrentBuilding).ToList();
        }

        //get all electrial records in timeframe with or without concer for the current building
        public IEnumerable<ElectricalRecord> GetElectricalRecords(DateTime start, DateTime end)
        {
            //return a list of eletrical records within or equal to the timeframe without concern for current building.
            if(CurrentBuilding == null)
            {
                return _appDbContext.EletricalRecords.Where(e => e.RecordedDateTime >= start && e.RecordedDateTime <= end).ToList();
            }

            //else return a list of eletrical records within or equal to the timeframe with concern for current building.
            return _appDbContext.EletricalRecords.Where(e => e.RecordedDateTime >= start && e.RecordedDateTime <= end).ToList();
        }


    }
}
