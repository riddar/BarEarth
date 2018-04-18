﻿using BarEarth.Data;
using BarEarth.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarEarth.Controllers
{
    public class TableController : Controller
    {
        private readonly ApplicationDbContext context;

        public TableController(ApplicationDbContext _context)
        {
            context = _context;
        }

        [HttpGet, ActionName("Table")]
        public async Task<IActionResult> TableGet(string sortOrder, string address)
        {
            var Bars = await context.Bars.ToListAsync();

            ViewBag.BarName = sortOrder == "BarName" ? "BarName_desc" : "BarName";
            ViewBag.Price = sortOrder == "Price" ? "Price_desc" : "Price";
            ViewBag.Distance = sortOrder == "Distance" ? "Distance_desc" : "Distance";
            ViewBag.Comments = sortOrder == "Comment" ? "Comments_desc" : "Comment";
            ViewBag.Rating = sortOrder == "Rating" ? "Rating_desc" : "Rating";

            switch (sortOrder)
            {
                case "BarName":
                    Bars = Bars.OrderBy(b => b.Name).ToList();
                    break;
                case "BarName_desc":
                    Bars = Bars.OrderByDescending(b => b.Name).ToList();
                    break;
                case "Price":
                    Bars = Bars;
                    break;
                case "Price_desc":
                    Bars = Bars;
                    break;
                case "Distance":
                    Bars = Bars;
                    break;
                case "Distance_desc":
                    Bars = Bars;
                    break;
                case "Comment":
                    Bars = Bars.OrderBy(b => b.Ratings.Count()).ToList();
                    break;
                case "Comment_desc":
                    Bars = Bars.OrderByDescending(b => b.Ratings.Count()).ToList();
                    break;
                case "Rating":
                    Bars = Bars;
                    break;
                default:
                    break;
            }
                    return View(Bars);
        }

        [HttpPost, ActionName("Table")]
        public IActionResult TablePost(Bar bar)
        {
            return RedirectToAction("Bar", "Bar", bar);
        }
    }
}