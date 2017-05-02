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
 * File: AdminController.cs
 * Purpose: to run the home, about and contact views of the website.    
 * 
 * *******************************************************************************************************************/
using System;
using Microsoft.AspNetCore.Mvc;
using EngineeringOnDisplay2017.Models;


namespace EngineeringOnDisplay2017.Controllers
{

    public class MainController : Controller
    {
        private IGraphRepository _graphRepository;

        
        //constructor that gets a copy of the graphRespository
        public MainController(IGraphRepository graphRepository)
        {
            _graphRepository = graphRepository;
        }
        

        //Starting page for the Single page application for the website. Everything but what is in the Admin side.
        public IActionResult Index()
        {
            return View();
        }

        
        //get graphPoints(SensorType) will return all Points in database for given sensor type
        public JsonResult GetAllGraphPoints()
        {
            Response.ContentType = "application/json";

            return Json(_graphRepository.GetGraphPoints(SensorType.Electrical));
        }

        //get a custom set of graph points from the parameters
        public JsonResult GetGraphPoints(DateTime end, int numTicks, SensorType sensor, SensorData dataType, GraphScale scale)
        {
            return Json(_graphRepository.GetGraphPoints(end, numTicks, sensor, dataType, scale));
        }
        
        //get the image tags for the full sized images
        public IActionResult LoadSlideShow()
        {
            //todo, create an repository that pulls the img tags from a database.
            return View("SlideShow");
        }

    
        //get the main display page for the charting part of website
        public IActionResult LoadContent(string name)
        {
            return View(name);
        }
    }
}

            