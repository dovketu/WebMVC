using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.EntitiesConfigurations;
using WebMVC.Models;
using WebMVC.ViewModels;

namespace WebMVC
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts) { }
        public DbSet<ExpCategory> ExpCategory { get; set; }
        public DbSet<Expenses> Expenses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ExpCategory>().HasData(
                new ExpCategory() { Id = 1, Category = "Komunalinės paslaugos" },
                new ExpCategory() { Id = 2, Category = "Išlaidos automobiliui" }

            
            );

            modelBuilder.ApplyConfiguration<Expenses>(new ExpensesConfiguration());
            modelBuilder.ApplyConfiguration<ExpCategory>(new ExpCategoryConfiguration());


        }

    }
}
