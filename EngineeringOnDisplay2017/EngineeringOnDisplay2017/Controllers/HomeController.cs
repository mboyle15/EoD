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
 * File: HomeController.cs
 * Purpose: to run the home, about and contact views of the website.    
 * 
 * *******************************************************************************************************************
 * Methods
 * 
 *  
 *  *************************************************************************************************************
 * Change Log
 * 
 * **************************************************************************************************************
 * ToDo
 * 
 *  Write out puedo-code methods in the section above and create views as needed.  
 * **************************************************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EngineeringOnDisplay2017.Controllers
{
    public class HomeController : Controller
    {


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
    }
}
