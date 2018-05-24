using BarEarth.Data;
using BarEarth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarEarth.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext context;
        private UserManager<ApplicationUser> userManager;

        public AdminController(ApplicationDbContext _context, UserManager<ApplicationUser> manager)
        {
            context = _context;
            userManager = manager;
        }

        [HttpGet]
        public async Task<IActionResult> Bars()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            var bars = await context.Bars
                .Where(b => b.User == user)
                .Include(b => b.Ratings)
                .Include(b => b.Products)
                .ToListAsync();

            return View(bars);
        }

        [HttpGet, Route("Admin/BarEdit")]
        public async Task<ActionResult> BarEdit(int? id)
        {
            if (id == null)
                return View();

            var bar = await context.Bars
                .Where(b => b.Id == id)
                .Include(b => b.Ratings)
                .Include(b => b.Products)
                .FirstOrDefaultAsync();

            return View(bar);
        }

        [HttpPost, Route("Admin/BarEdit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> BarEditPost(Bar bar)
        {
            if (bar == null)
                return View();

            context.Update(bar);
            await context.SaveChangesAsync();

            bar = await context.Bars
                .Where(b => b.Id == bar.Id)
                .Include(b => b.Ratings)
                .Include(b => b.Products)
                .FirstOrDefaultAsync();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet, Route("Admin/ProductsEdit")]
        public async Task<ActionResult> ProductsEdit(int? id)
        {
            if (id == null)
                return View();

            var bar = await context.Bars
                .Where(b => b.Id == id)
                .Include(b => b.Ratings)
                .Include(b => b.Products)
                .FirstOrDefaultAsync();

            return View(bar);
        }

        [HttpGet, Route("Admin/SaveProduct")]
        public async Task<ActionResult> SaveProduct(Product product)
        {
            if (product == null)
                return RedirectToAction("Index", "Home");

            product = await context.Products
                .Include(p => p.Bar)
                .SingleOrDefaultAsync(m => m.Id == product.Id);

            var bar = await context.Bars
                .Where(b => b == product.Bar)
                .Include(b => b.Products)
                .Include(b => b.Ratings)
                .FirstOrDefaultAsync();

            context.Update(product);
            await context.SaveChangesAsync();

            return View("ProductsEdit", bar);
        }

        [HttpGet, Route("Admin/DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(int? id)
        {
            if (id == null)
                return RedirectToAction("Index", "Home");

            var product = await context.Products
                .Include(p => p.Bar)
                .SingleOrDefaultAsync(m => m.Id == id);

            var bar = await context.Bars
                .Where(b => b == product.Bar)
                .Include(b => b.Products)
                .Include(b => b.Ratings)
                .FirstOrDefaultAsync();
            
            if (product == null)
                return View();

            context.Products.Remove(product);
            await context.SaveChangesAsync();

            return View("ProductsEdit", bar);
        }

        [HttpGet, Route("Admin/CreateProduct")]
        public IActionResult CreateProduct(int? id)
        {
            if(id.HasValue)
                ViewData["BarId"] = id;

            return View();
        }

        [HttpPost, Route("Admin/CreateProduct")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            var bar = await context.Bars
                .Where(b => b.Id == product.BarId)
                .FirstOrDefaultAsync();

            product.Bar = bar;

            if (ModelState.IsValid)
            {
                context.Add(product);
                await context.SaveChangesAsync();
                return View("ProductsEdit", bar);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet, Route("Admin/RatingsEdit")]
        public async Task<ActionResult> RatingsEdit(int? id)
        {
            if (id == null)
                return View();

            var bar = await context.Bars
                .Where(b => b.Id == id)
                .Include(b => b.Ratings)
                .Include(b => b.Products)
                .FirstOrDefaultAsync();

            var Ratings = await context.Ratings
                .Where(r => r.Bar == bar)
                .ToListAsync();

            return View(Ratings);
        }

        [HttpGet, Route("Admin/DeleteRating")]
        public async Task<IActionResult> DeleteRating(int? id)
        {
            if (id == null)
                return RedirectToAction("Index", "Home");

            var rating = await context.Ratings
                .Include(r => r.Bar)
                .SingleOrDefaultAsync(r => r.Id == id);

            var bar = await context.Bars
                .Where(b => b == rating.Bar)
                .Include(b => b.Products)
                .Include(b => b.Ratings)
                .FirstOrDefaultAsync();

            if (rating == null)
                return View();

            context.Ratings.Remove(rating);
            await context.SaveChangesAsync();

            return View("RatingsEdit", bar);
        }
    }
}
