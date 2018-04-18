using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BarEarth.Models;
using Microsoft.AspNetCore.Mvc;

namespace BarEarth.Controllers
{
    public class MapController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Map(Bar bar)
        {
            ViewData["Info"] = "test";

            return View();
        }
    }
}