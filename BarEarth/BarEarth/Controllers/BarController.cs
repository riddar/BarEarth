using BarEarth.Data;
using BarEarth.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private UserManager<ApplicationUser> userManager;

        public BarController(ApplicationDbContext _context, UserManager<ApplicationUser> manager)
        {
            context = _context;
            userManager = manager;
        }

        [Route("Bar/bar")]
        public ActionResult Bar(int? id)
        {
            if (id == null)
                return View();

            Bar Bar = context.Bars.Include(b => b.Products).FirstOrDefault(b => b.Id == id);

            if (Bar == null)
                return View();

            ViewBag.BarId = id.Value;

            var comments = context.Ratings.Where(d => d.Bar.Id == id).ToList();
            ViewBag.Comments = comments;

            var ratings = context.Ratings.Where(d => d.Bar.Id == id).ToList();
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

        [Route("Bar/barName")]
        public async Task<ActionResult> BarName(string name)
        {
            if (name == null)
                return View();

            Bar bar = await context.Bars.Where(b => b.Name == name).Include(b => b.Ratings).Include(b => b.Products).FirstOrDefaultAsync();

            ViewBag.BarId = bar.Id;

            var comments = context.Ratings.Where(d => d.Bar.Id == bar.Id).ToList();
            ViewBag.Comments = comments;

            var ratings = context.Ratings.Where(d => d.Bar.Id == bar.Id).ToList();
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

            return View("Bar", bar);
        }

        [Route("Bar/details")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return View() ;
            }
            Bar Bar = context.Bars.Include(b => b.Products).FirstOrDefault(b => b.Id==id);

            
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
        public ActionResult Add(IFormCollection form, string comment)
        {
            var review = form["Comment"].ToString();
            var barId = int.Parse(form["BarId"]);
            var rating = int.Parse(form["Rating"]);

            Rating barComment = new Rating
            {
                BarId = barId,
                Rate = rating,
                DateTime = DateTime.Now
            };

            try
            {
                barComment.UserName = userManager.GetUserName(HttpContext.User);
                barComment.Review = review;
            }
            catch (Exception)
            {
                barComment.UserName = "anonymous";
                barComment.Review = null;
            }

            context.Ratings.Add(barComment);
            context.SaveChanges();
            return RedirectToAction("Bar", "Bar", new { id = barId });
        }
    }
}
