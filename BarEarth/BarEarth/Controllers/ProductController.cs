using BarEarth.Data;
using BarEarth.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarEarth.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext context;

        public ProductController(ApplicationDbContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public IActionResult Product()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AddProduct(Bar _bar)
        {
            if (_bar == null)
                return NotFound();

            var bar = await context.Bars
                .Include(b => b.Ratings)
                .Where(b => b.Id == _bar.Id)
                .FirstOrDefaultAsync();

            if (bar == null)
                return NotFound();

            return View(bar);
        }

        [HttpPost, ActionName("AddProduct")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProductPost(Product product)
        {
            if (product == null)
                return NotFound();

            
        }

    }
}
