using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BarEarth.Models;

namespace BarEarth.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Bar> Bars { get; set; }
        public DbSet<Rating> Ratings { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            //bar
            builder.Entity<Bar>().HasKey(b => b.Id);
            builder.Entity<Bar>().Property(b => b.Name).IsRequired().HasMaxLength(50);
            builder.Entity<Bar>().Property(b => b.AgeRestriction).IsRequired();
            builder.Entity<Bar>().Property(b => b.Description).HasMaxLength(160);
            builder.Entity<Bar>().Property(b => b.Website).HasMaxLength(100);
            builder.Entity<Bar>().Property(b => b.Email).HasMaxLength(255);
            builder.Entity<Bar>().HasMany(b => b.Ratings).WithOne(b => b.Bar).IsRequired();

            //ratings
            builder.Entity<Rating>().HasKey(r => r.Id);
            builder.Entity<Rating>().HasOne(r => r.Bar).WithMany(r => r.Ratings).IsRequired();
        }
    }
}
