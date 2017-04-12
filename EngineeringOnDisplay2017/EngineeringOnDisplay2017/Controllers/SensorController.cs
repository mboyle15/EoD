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
 * File: SensorController.cs
 * Purpose: To handle the logic behind creating a sensor view.  This controller will connect to the SQL database holding
 *          Sensor information and submit SQL queiries then parse and inject the data into the view for each sensor.
 * 
 * *******************************************************************************************************************
 * Methods
 * 
 *  ConnectToDatabase
 *      Purpose: Brute force method of connecting to database.  Everything will be hard coded.  Need to implement 
 *          Entiy framework but this should be suffecent for this project.
 *      Inputs:
 *      Outputs:
 * 
 *  ElectricalUsage
 *      Purpose:   
 *      Inputs:
 *      Outputs:
 *  
 *  *************************************************************************************************************
 * Change Log
 * 4/4/17   Terrance Mount  Created the file.  Added stubs for each of the methods listed in the above puedo code
 * 
 * **************************************************************************************************************
 * ToDo:  Write full methods for each of the puedo-code Methods.
 * 
 * **************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EngineeringOnDisplay2017.Models;
using EngineeringOnDisplay2017.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EngineeringOnDisplay2017.Controllers
{
    public class SensorController : Controller
    {
        private IElectricalRepository _eletricalRepository;

        public SensorController(IElectricalRepository electricalRepository)
        {
            _eletricalRepository = electricalRepository;
        }

        //controller for the EletricalUsage View
        public IActionResult ElectricalUsage()
        {
            //create an instace of the SensorViewModel that has all the models wrapped inside.
            SensorViewModel sensorViewModel = new SensorViewModel();
            sensorViewModel.ElectricalRecords = _eletricalRepository.GetElectricalRecords();

            return View(sensorViewModel);
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
