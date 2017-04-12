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

namespace EngineeringOnDisplay2017.Models
{
    /**
     * Model for storing a record (row in table) of eletrical sensor data.
     */
    public class ElectricalRecord
    {
        public int Id { get; set; }  //primary key for record
        public DateTime RecordedDateTime { get; set; }  //when is this data recorded
        public float Usage { get; set; }  //usage in kilowatthours  (don't know when this number gets reset to zero)
        public float Demand { get; set; }   //demand in kilowatts 
        public BuildingRecord BuildingRecord { get; set; } //a reference to the foriegn key (don't really understand how this works.
    }
}
