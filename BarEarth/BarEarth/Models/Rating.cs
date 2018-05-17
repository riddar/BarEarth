using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BarEarth.Models
{
    [Authorize]
    public class Rating
    {
        public int Id { get; set; }
        public int BarId { get; set; }

        public int Rate { get; set; }
        public string Review { get; set; }
        public string UserName { get; set; }
        public DateTime DateTime { get; set; }

        public Bar Bar { get; set; }
        
    }
}
