using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EngineeringOnDisplay2017.Models;
using System.Collections;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EngineeringOnDisplay2017.Controllers.Api
{
    [Route("users")]
    public class UsersController : Controller
    {
        private AppDbContext _appDbContext;

        //constructor to get a copy of AppDbContext through dependency injection from MVC
        public UsersController(AppDbContext context)
        {
            _appDbContext = context;
        }

        [HttpGet()]
        public IActionResult GetUsers()
        {

            //get a list of users from the injected context
            IEnumerable users = _appDbContext.Users.ToList();


            //map the list of users to a UserForListViewModel

            return Ok(users);
        }
    }
}
