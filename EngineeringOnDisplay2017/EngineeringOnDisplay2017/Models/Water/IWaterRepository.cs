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
 * File: IWaterRepository.cs
 * Purpose: Interface for the exposed methods and properties to be used by the controllers and views.
 *          Look at the corrosponding class that implements this interface for more details.  
 * *******************************************************************************************************************/
using System;
using System.Collections.Generic;

namespace EngineeringOnDisplay2017.Models
{
    public interface IWaterRepository
    {
        BuildingRecord CurrentBuilding { get; set; } //building the linq queires will be run agains if this property is set

        IEnumerable<WaterRecord> GetWaterRecords(); //returns all records with or without regaurds to building

        IEnumerable<WaterRecord> GetWaterRecords(DateTime start, DateTime end); //returns all records within the timespan with or without regaurds to building

        WaterRecord GetWaterRecordById(int eletricalRecordId); //returns record accouding to id

    }
}
