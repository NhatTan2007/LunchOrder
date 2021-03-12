using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LunchOrderManagement.Controllers
{
    //[ApiController]
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("/error404")]
        public IActionResult Notfound()
        {
            return View("~/Views/Errors/404.cshtml");
        }
    }
}
