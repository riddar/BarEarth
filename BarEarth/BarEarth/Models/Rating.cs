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
        [Key]
        public int Id { get; set; }
        [MaxLength(5)]
        public int Rate { get; set; }
        [StringLength(160)]
        public string Review { get; set; }
        [ForeignKey("BarId")]
        public Bar Bar { get; set; }
        public string UserName { get; set; }
        
    }
}
