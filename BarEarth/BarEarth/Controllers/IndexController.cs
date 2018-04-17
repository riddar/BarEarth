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

        [HttpPost, ActionName("IndexMapPost")]
        public IActionResult IndexMapPost(string address)
        {
            return RedirectToAction("Map", "Map", address);
        }

        [HttpPost, ActionName("IndexTablePost")]
        public IActionResult IndexTablePost(string address)
        {
            return RedirectToAction("Table", "Table", address);
        }
    }
}