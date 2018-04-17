using BarEarth.Data;
using BarEarth.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarEarth.Controllers
{
    public class BarController: Controller
    {
        private readonly ApplicationDbContext context;

        public BarController(ApplicationDbContext _context)
        {
            context = _context;
        }

        [HttpGet, ActionName("Bar")]
        public IActionResult BarGet(Bar bar)
        {
            if (BarExists(bar.Id))
                return NotFound();

            return View(bar);
        }

        private bool BarExists(int id)
        {
            return context.Bars.Any(b => b.Id == id);
        }
    }
}
