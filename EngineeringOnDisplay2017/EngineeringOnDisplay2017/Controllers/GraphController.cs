using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EngineeringOnDisplay2017.Models;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EngineeringOnDisplay2017.Controllers
{
    //[Route("api/[controller]")]
    public class GraphController : Controller
    {
        private IGraphRepository _graphRepository;

        public GraphController(IGraphRepository graphRepository)
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
    }
}
