using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EngineeringOnDisplay2017.Models;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EngineeringOnDisplay2017.Controllers
{

    public class MainController : Controller
    {
        private IGraphRepository _graphRepository;

        public MainController(IGraphRepository graphRepository)
        {
            _graphRepository = graphRepository;
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
        public IActionResult LoadSlideShowFull()
        {
            //todo, create an repository that pulls the img tags from a database.
            return View();
        }

        public IActionResult LoadSlideShowNavBar()
        {
            //todo, create a repository that pulls imgs from database.
            return View();
        }

        //get the main display page for the charting part of website
        public IActionResult LoadContent(string name)
        {
            return View(name);
        }
    }
}

            