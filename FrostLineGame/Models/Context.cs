using FrostLineGame.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrostLineGames.Models
{
    public class Context:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=DESKTOP-S12B08P;database=ECommerrce;integrated security=true;Trusted_Connection=true;TrustServerCertificate=False;Encrypt=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products{ get; set; }
        public DbSet<Discount> Discounts{ get; set; }
        public DbSet<DiscountCategory> DiscountCategories{ get; set; }
        public DbSet<User> Users{ get; set; }
        
    }
}
