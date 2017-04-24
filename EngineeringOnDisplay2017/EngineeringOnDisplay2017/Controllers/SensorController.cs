/**
 * 
 **/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EngineeringOnDisplay2017.Models;
using EngineeringOnDisplay2017.ViewModels;


namespace EngineeringOnDisplay2017.Controllers
{
    public class SensorController : Controller
    {
        private ISensorRepository _sensorRepository;
         
        /**
         * simple constructor that stores a copy of the sensor repository.  
         **/
        public SensorController(ISensorRepository sensorRepository)
        {
            _sensorRepository = sensorRepository;
        }

        /**
         * Get a full graph website with standard layout
         * @returns the call to Graph(layoutHidden) to not hide the layout
         **/
        public IActionResult Graph()
        {

           return Graph(false);
        }

        public IActionResult Graph(bool LayoutHidden)
        {
            
            if (LayoutHidden)
            {
                ViewBag.Layout = "";
                
            }
            else
            {
                ViewBag.Layout = "_Layout.cshtml";
            }

            return View();
        }

        //create a test view for the sensors
        public IActionResult SensorTest()
        {


           
            
           
            return View();
        }

        public IActionResult SensorData(string SenorType, string Scale)
        {


            return View();
        }


        public IActionResult DataTest()
        {
            //ViewBag.ConsoleOutput = _sensorRepository.TestConsole();


            //return View(_sensorRepository.QueryTests());

            return View();
        }

        public IActionResult AjaxTest()
        {

            return View();
        }




























        //controller for the EletricalUsage View
        public IActionResult ElectricalUsage()
        {
            
            return View();
        }

        public IActionResult WaterUsage()
        {
            return View();
        }
        public IActionResult NatGasUsage()
        {
            return View();
        }
        public IActionResult OutsideTemperature()
        {
            return View();
        }
    }
}
