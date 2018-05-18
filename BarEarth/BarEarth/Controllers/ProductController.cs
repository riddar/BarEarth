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

        [HttpGet, ActionName("Products")]
        public async Task<IActionResult> ProductsGet(string sortOrder)
        {
            var products = await context.Products.ToListAsync();

            ViewBag.BarName = sortOrder == "Name" ? "Name_desc" : "Name";
            ViewBag.Price = sortOrder == "Price" ? "Price_desc" : "Price";
            ViewBag.Type = sortOrder == "Type" ? "Type_desc" : "Type";
            ViewBag.SubType = sortOrder == "SubType" ? "SubType_desc" : "SubType";

            switch (sortOrder)
            {
                case "Name":
                    products = products.OrderBy(p => p.Name).ToList();
                    break;
                case "Name_desc":
                    products = products.OrderByDescending(p => p.Name).ToList();
                    break;
                case "Price":
                    products = products.OrderBy(p => p.Price).ToList();
                    break;
                case "Price_desc":
                    products = products.OrderByDescending(p => p.Price).ToList();
                    break;
                case "Type":
                    products = products.OrderBy(p => p.Type).ToList();
                    break;
                case "Type_desc":
                    products = products.OrderByDescending(p => p.Type).ToList();
                    break;
                case "SubType":
                    products = products.OrderBy(p => p.SubType).ToList();
                    break;
                case "SubType_desc":
                    products = products.OrderByDescending(p => p.SubType).ToList();
                    break;
                default:
                    break;
            }
            return View(products);
        }

    }
}
