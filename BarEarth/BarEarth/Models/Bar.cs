using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BarEarth.Models
{
    [AllowAnonymous]
    public class Bar
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        public int AgeRestriction { get; set; }
        [StringLength(160)]
        public string Description { get; set; }
        public string Type { get; set; }
        [StringLength(100)]
        public string Website { get; set; }
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
        [StringLength(255)]
        public string Email { get; set; }
        public string FaceBook { get; set; }
        public string PlaceId { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }

        public virtual IList<Rating> Ratings { get; set; }
    }
}
