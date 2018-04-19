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
            Bar2.Name = bar.Name;
            _context.Bars.Add(Bar2);
            _context.SaveChanges();

            return Json(bar);
        }
        
        
    }
}
