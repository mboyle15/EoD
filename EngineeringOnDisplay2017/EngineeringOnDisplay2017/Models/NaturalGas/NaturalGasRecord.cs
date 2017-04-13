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
 * File: NaturalGasRecord.cs
 * Purpose: Model for the NaturalGas sensor in buildings
 * 
 * *******************************************************************************************************************/

using System;
using System.ComponentModel.DataAnnotations;

namespace EngineeringOnDisplay2017.Models
{
    public class NaturalGasRecord
    {
        [Required]
        public int NaturalGasRecordId { get; set; } //primary key for record

        [Required]
        public DateTime RecordedDateTime { get; set; }  //time record was created

        public float Usage { get; set; } //Natural Gas usage in ccf or 100 cubic feet of natural gas

        public BuildingRecord BuildingRecord { get; set; }  //reference to that building record.
    }
}
