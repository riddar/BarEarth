using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BarEarth.Models
{
    [AllowAnonymous]
    public class Bar
    {
      
        public int Id { get; set; }
        public string Name { get; set; }
        public int AgeRestriction { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }
        public string PlaceId { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string Address { get; set; }
        public string OpeningHours { get; set; }
        public string PhoneNumber { get; set; }
        public string PhotoReference { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        public int TotalRating { get; set; }
        public int AmountOfStars { get; set; }

        public virtual IList<Rating> Ratings { get; set; }
        public virtual IList<Product> Products { get; set; }
    }
}
