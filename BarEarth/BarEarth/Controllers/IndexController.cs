using BarEarth.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;
using BarEarth.Models;
using Microsoft.EntityFrameworkCore;

namespace BarEarth.Controllers
{
    public class IndexController: Controller
    {
        private readonly ApplicationDbContext context;

        public IndexController(ApplicationDbContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
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
            //fix later
            return true;
        }
    }
}