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
 * File: SensorRecord.cs
 * Purpose: Define the sensor data to store in the database.  This class will be the primary model used in the website.
 * 
 * *******************************************************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EngineeringOnDisplay2017.Models
{

    /**
     * Define the sensor data to store in the database.  This class will be the primary model used in the website.
     */
    public class SensorRecord
    {
        public int SenorDataId { get; set; }  //primary key for this record //should be every 15minutes to 1 hour.
        public float EletricalUsage { get; set; } //eletrical usage in kWh.  Need to see what is the timeframe for this.
        public float WaterUsage { get; set; } //water usage in gallons.
        public float NaturalGasUsage { get; set; }  //Natural gas usage
        public float OutsideTemperature { get; set; } //outside temperature 
        public int BuildingId { get; set; } //foreign key for the building id incase there is more then one building records
    }
}