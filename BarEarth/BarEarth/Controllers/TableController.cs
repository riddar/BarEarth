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
        public ActionResult TableGet(string sortOrder, string address)
        {
            var Bars = context.Bars.ToList();

            ViewBag.BarName = sortOrder == "BarName" ? "BarName_desc" : "BarName";
            ViewBag.Price = sortOrder == "Price" ? "Price_desc" : "Price";
            ViewBag.Distance = sortOrder == "Distance" ? "Distance_desc" : "Distance";
            ViewBag.Comments = sortOrder == "Comment" ? "Comments_desc" : "Comment";
            ViewBag.Rating = sortOrder == "Rating" ? "Rating_desc" : "Rating";


            for (int i = 0; i < Bars.Count; i++)
            {
                var ratings = context.Ratings.Where(d => d.Bar.Id == Bars[i].Id).ToList();

                if (ratings.Count() > 0)
                {
                    var bar = Bars[i];
                    var barRatings = context.Ratings.Where(r => r.Id == bar.Id);



                    var ratingSum = barRatings.Select(x => x.Rate).Sum();

                    ViewBag.RatingSum = ratingSum;
                    var ratingCount = barRatings.Count();
                    ViewBag.RatingCount = ratingCount;

                    bar.TotalRating = ratingSum;
                    bar.AmountOfStars = ratingCount;

                    context.Bars.Update(bar);

                    context.SaveChanges();
                }
                else
                {
                    ViewBag.RatingSum = 0;
                    ViewBag.RatingCount = 0;
                }

            }

            switch (sortOrder)
            {
                case "BarName":
                    Bars = Bars.OrderBy(b => b.Name).ToList();
                    break;
                case "BarName_desc":
                    Bars = Bars.OrderByDescending(b => b.Name).ToList();
                    break;
                case "Price":
                    Bars = Bars; //add products first
                    break;
                case "Price_desc":
                    Bars = Bars; //add products first
                    break;
                case "Comment":
                    Bars = Bars.OrderBy(b => b.Ratings.Count()).ToList();
                    break;
                case "Comment_desc":
                    Bars = Bars.OrderByDescending(b => b.Ratings.Count()).ToList();
                    break;
                case "Rating":
                    Bars = Bars.OrderBy(b => b.Ratings.Average(rating => rating.Rate)).ToList();
                    break;
                case "Rating_desc":
                    Bars = Bars.OrderByDescending(b => b.Ratings.Average(rating => rating.Rate)).ToList();
                    break;
                default:
                    break;
            }
            return View(Bars);
        }

        [HttpPost, ActionName("Table")]
        public IActionResult TablePost(Bar bar)
        {
            if (bar == null)
                return NotFound();

            return RedirectToAction("Bar", "Bar", bar);
        }
    }
}
