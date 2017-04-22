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
 * File: IBuildingRepository.cs
 * Purpose: Interface for the exposed methods and properties to be used by the controllers and views.
 *          Look at the corrosponding class that implements this interface for more details.  
 * *******************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EngineeringOnDisplay2017.Models
{
    public interface IBuildingRepository
    {
        IEnumerable<BuildingRecord> GetBuildingRecords();   //get a list of all Buildings in the Building table

        BuildingRecord GetBuildingRecordById(int recordId);  //get a building by primay key Id

        void SetBuildingRecord(BuildingRecord newBuilding); //add a new building to the database

        void SetBuildingRecord(IEnumerable<BuildingRecord> newBuildings); //add a list of new buildings to the database  
    }
}
