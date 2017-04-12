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
 * Purpose: Interface for the exposed methods and properties to be used by the views.
 * 
 * Puedo-code:
 *      
 *      EletricalRecords - returns a list of Eletrical records without any help.
 *      
 *      Get Eletrical Records in TimeSpan - return a list of records inbetween a start and end TimeDate and with a buildingId
 *          **Example for 12 month graph.
 *              Start = DateTime.Now() - DateTime.AddMonth(-12)
 *              End = DateTime.Now()
 *              Building = buildingRecord obtianed through the building Repository.
 *      
 *      
 *      Get Building for Electrical Record - incase you need to know what is the building assocated with eletrical record
 *      
 *      
 *          
 * 
 * 
 * *******************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EngineeringOnDisplay2017.Models;

namespace EngineeringOnDisplay2017.Models
{
    public interface IElectricalRepository
    {
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
         * Sets the CurrentBuilding to null so queries will not take the building into account.
         */
        void ClearBuilding();

        /**
          * Gets all records in ElectricalRecords table
          * @return  Collection of electrical records
          */
        ElectricalRecord GetElectricalRecordById(int recordId);
        
    }
}
