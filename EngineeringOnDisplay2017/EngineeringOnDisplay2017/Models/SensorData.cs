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
 * File: SensorData.cs
 * Purpose:    
 * 
 * *******************************************************************************************************************
 * Methods
 * None, this class is simply to be a container for the records in the database.  
 *  
 *  *************************************************************************************************************
 * Change Log
 * 
 * **************************************************************************************************************
 * ToDo
 *  Write class
 * **************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EngineeringOnDisplay2017.Models
{
    public class SensorData
    {
        public int SenorDataId { get; set; }  //primary key for this record
        public float EletricalKiloWatts { get; set; } //Hourly average of the eletrical kiloWatts.  Should only have 12 entries max per day.
        public float EletricalKiloWattHours { get; set; } //Hourly aver of kiloWattHours.
        public float WaterGallons { get; set; }
        public float MyProperty { get; set; }
    }
}
