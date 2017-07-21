using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EngineeringOnDisplay2017.Controllers.Api
{
    [Route("api/users")]
    public class UsersController : Controller
    {
        
        
        public IActionResult GetUsers()
        {
            return View();
        }
    }
}
