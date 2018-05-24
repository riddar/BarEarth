using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BarEarth.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        public int Price { get; set; }
        public string Type { get; set; }
        public string SubType { get; set; }
        public string PictureUrl { get; set; }
        public Boolean IsFood { get; set; }
        [ForeignKey("BarId")]
        public Bar Bar { get; set; }

        public int BarId { get; set; }
    }
}
