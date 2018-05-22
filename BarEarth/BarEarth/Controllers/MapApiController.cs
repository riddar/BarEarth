using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BarEarth.Data;
using BarEarth.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BarEarth.Controllers
{
    [Produces("application/json")]
    public class MapApiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MapApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        //public void ReceiveBarData(? data)
        // GET: api/MapApi
        // GET: api/MapApi/5

        Bar Bar2 = new Bar();
     
        public JsonResult Get(Bar bar)
        {
            var existingBar = _context.Bars.Where(v => v.PlaceId == bar.PlaceId);

            var count = existingBar.Count();

            if (count==0)
            {
                Bar2.Name = bar.Name;
                Bar2.PlaceId = bar.PlaceId;
                Bar2.Address = bar.Address;
                Bar2.OpeningHours = bar.OpeningHours;
                Bar2.Website = bar.Website;
                Bar2.Type = bar.Type;
                Bar2.PhoneNumber = bar.PhoneNumber;
                Bar2.PhotoReference = bar.PhotoReference;
                Bar2.Longitude = bar.Longitude;
                Bar2.Latitude = bar.Latitude;
                _context.Bars.Add(Bar2);
                _context.SaveChanges();

            }
            return Json(bar);
        }
        
        
    }
}
