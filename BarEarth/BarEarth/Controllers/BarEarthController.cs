using BarEarth.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace BarEarth.Controllers
{
    public class BarEarthController: Controller
    {
        private readonly ApplicationDbContext context;

        public BarEarthController(ApplicationDbContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

    }
}