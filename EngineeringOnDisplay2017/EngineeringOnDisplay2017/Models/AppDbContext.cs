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
 * File: AppDbContext.cs
 * Purpose: The glue between the models and the database.  Full of properties for every table in database which is mapped
 *      to models.  Enity Framework needs this class to run Linq queries.  This Context class is essentcial to creating
 *      package manager Migrations for database in the code first approach.  
 * 
 * *******************************************************************************************************************/
using Microsoft.EntityFrameworkCore;

namespace EngineeringOnDisplay2017.Models
{

    /**
     * Allows for loose connection with services in all aspects of MVC
     */
    public class AppDbContext : DbContext
    {
        //constructor needed to give superclass the options passed in from startup.cs  
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)  { }

        //db connection for the building records
        public DbSet<BuildingRecord> BuildingRecords { get; set; } 

        //db connection for the electrical records
        public DbSet<ElectricalRecord> ElectricalRecords { get; set; }

        //db connection for natural gas records
        public DbSet<NaturalGasRecord> NaturalGasRecords { get; set; }

        //db connection for water records
        public DbSet<WaterRecord> WaterRecords { get; set; }

        //db connection for outside temperature records
        public DbSet<OutsideTempRecord> OutsideTempRecords { get; set; }

        public DbSet<Slide> Slides { get; set; }
    }
} 
