﻿using BarEarth.Data;
using BarEarth.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

       [Route("Bar/details")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return View() ;
            }
            Bar Bar = context.Bars.FirstOrDefault(b => b.Id==id);

            
            if (Bar == null)
            {
                return View();
            }
            ViewBag.BarId = id.Value;

            var comments = context.Ratings.Where(d => d.Bar.Id==id).ToList();
            ViewBag.Comments = comments;

            var ratings = context.Ratings.Where(d => d.Bar.Id==id).ToList();
            if (ratings.Count() > 0)
            {
                var ratingSum = ratings.Select(d => d.Rate).Sum();
                ViewBag.RatingSum = ratingSum;
                var ratingCount = ratings.Count();
                ViewBag.RatingCount = ratingCount;
            }
            else
            {
                ViewBag.RatingSum = 0;
                ViewBag.RatingCount = 0;
            }

            return View(Bar);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(IFormCollection form)
        {
            var review = form["Comment"].ToString();
            var barId = int.Parse(form["BarId"]);
            var rating = int.Parse(form["Rating"]);

            Rating barComment = new Rating()
            {
               BarId = barId,
                Review = review,
                Rate = rating,
                DateTime = DateTime.Now
            };

            context.Ratings.Add(barComment);
            context.SaveChanges();

            return RedirectToAction("Details", new { id = barId });
        }
    }
}
