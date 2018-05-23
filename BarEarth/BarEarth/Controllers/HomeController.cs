using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BarEarth.Models;
using BarEarth.Data;
using Microsoft.AspNetCore.Identity;

namespace BarEarth.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext context;
   

        public HomeController(ApplicationDbContext _context)
        {
            context = _context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult IndexMapPost(string address)
        {
            if (AddressExists(address) == false)
                return NotFound();

            return RedirectToAction("Map", "Map", address);
        }

        [HttpPost]
        public IActionResult IndexTablePost(string address)
        {
            if (AddressExists(address) == false)
                return NotFound();

            return RedirectToAction("Table", "Table", address);
        }

        public bool AddressExists(string address)
        {
            return true;
        }
    }
}
