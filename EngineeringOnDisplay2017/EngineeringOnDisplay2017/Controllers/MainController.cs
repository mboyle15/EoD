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
        [HttpGet]
        public JsonResult GetAllGraphPoints()
        {
            Response.ContentType = "application/json";

            return Json(_graphRepository.GetGraphPoints(SensorType.Electrical));
        }

        /**
         * 
         * 
         * 
         **/
        public JsonResult GetGraphPoints(DateTime start, DateTime end, SensorType sensor, SensorData dataType, GraphScale scale)
        {
            //JQuery Request parameters
            //{ start: $canvasTag.attr("data-graph-start")},
            //{ end: $canvasTag.attr("data-graph-end")},
            //{ sensor: $canvasTag.attr("data-graph-sensor")},
            //{ dataType: $canvasTag.attr("data-graph-data")},
            //{ scale: $canvasTag.attr("data-graph-scale")}

            return Json(_graphRepository.GetGraphPoints(start, end, sensor, dataType, scale));
        }

        public IActionResult LoadContent(string name)
        {
            return View(name);
        }


        public IActionResult SensorTest()
        {
            return View();
        }
    }
}

            