using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace test.Controllers
{
    public class adsController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            DirectoryInfo d = new DirectoryInfo(@"C:\Users\andre\OneDrive\Documents\work\uni work\3rd year\semester 2\A470\project\function 3\test\test\wwwroot\images");
            FileInfo[] Files = d.GetFiles("*.jpg"); 
            string str = "";
            foreach (FileInfo file in Files)
            {
                str = str + "," + file.Name;
            }
            String[] imgFile = new string[3] { "images/test1.jpg", "images/test2.jpg", "images/test3.jpg" };
            ViewBag.imgFileList = str;
            return View();
        }
    }
}
