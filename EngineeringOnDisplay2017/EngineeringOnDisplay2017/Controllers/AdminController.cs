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
 * File: AdminController.cs
 * Purpose: Single page application for controlling the admin side for Engineering on Display.
 * *******************************************************************************************************************/
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using EngineeringOnDisplay2017.Models;
using System.Threading.Tasks;

namespace EngineeringOnDisplay2017.Controllers
{
    public class AdminController : Controller
    {
        private IEodRepository _repository;

        public AdminController(IEodRepository repository)
        {
            _repository = repository;
        }


        //this is the Admin Page that will launch the other pages
        public IActionResult Index()
        {

            return View();
        }

        [Authorize]
        public async Task<IActionResult> SlideShowManager()
        {
            return View(await _repository.GetSlidesAsync());
        }

        [Authorize]
        public IActionResult UserManager()
        {
            return View();
        }

        [Authorize]
        public IActionResult AddSlides()
        {
            return View();
        }


        
    }
}
