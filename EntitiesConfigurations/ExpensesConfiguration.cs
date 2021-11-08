using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Models;

namespace WebMVC.EntitiesConfigurations
{
    public class ExpensesConfiguration : IEntityTypeConfiguration<Expenses>
    {
        public void Configure(EntityTypeBuilder<Expenses> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired();
            builder.Property(a => a.ExpCategoryId).IsRequired();
            builder.Property(a => a.Name).IsRequired().HasMaxLength(50);
            builder.Property(a => a.Cost).IsRequired().HasPrecision(8, 2);

            builder.HasOne(p => p.ExpCategory)
            .WithMany(b => b.Expense)
            .HasForeignKey(p => p.ExpCategoryId)
            .OnDelete(DeleteBehavior.Restrict);

           
        }
    }
    
}
