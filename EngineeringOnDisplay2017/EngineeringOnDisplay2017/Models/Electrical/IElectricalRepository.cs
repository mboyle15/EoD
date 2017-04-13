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
 * File: IElectricalRepository.cs
 * Purpose: Interface for the exposed methods and properties to be used by the controllers and views.
 *          Look at the corrosponding class that implements this interface for more details.  
 * *******************************************************************************************************************/
using System;
using System.Collections.Generic;

namespace EngineeringOnDisplay2017.Models
{
    public interface IElectricalRepository
    {
        //Keep track of the current building and refrence it in all future queries as long as currentbuilding is valid. 
        BuildingRecord CurrentBuilding { get; set; }

        /**
         * Gets all records in ElectricalRecords table. If current building is set 
         * then it will return refering to building else it will return all in table.
         * @return  Collection of electrical records
         */
        IEnumerable<ElectricalRecord> GetElectricalRecords();
   
        /**
         * Gets electrical records between or equal to timeframe. If current building is set 
         * then it will return refering to building else it will return all in timeframe.
         * @param   start           Record Datetime must be larger or equal to start DateTime
         * @param   end             Record Datetime must be smaller or equal to end Datetime
         * @return                  Collection of electrical records
         */
        IEnumerable<ElectricalRecord> GetElectricalRecords(DateTime start, DateTime end);
        
        /**
          * Gets all records in ElectricalRecords table
          * @return  Collection of electrical records
          */
        ElectricalRecord GetElectricalRecordById(int eletricalRecordId);
        
    }
}
