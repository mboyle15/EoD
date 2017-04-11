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
 * File: SensorRecord.cs
 * Purpose: Model for the electrical sensor in buildings
 * 
 * *******************************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EngineeringOnDisplay2017.Models
{   
    /**
     * Model for storing a record (row in table) of eletrical sensor data.
     * Puedo-code: Properties for primary key, eletrical useage, demand and building id foreign key.    
     */
    public class ElectricalRecord
    {
        public uint RecordId { get; set; }  //primary key for record
        public float Usage { get; set; }  //usage in kilowatthours  (don't know when this number gets reset to zero)
        public float Demand { get; set; }   //demand in kilowatts 
        public byte BuildingId { get; set; }  //foriegn key for the record's building 
    }
}
