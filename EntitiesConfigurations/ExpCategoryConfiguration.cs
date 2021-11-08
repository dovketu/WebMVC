using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Models;

namespace WebMVC.EntitiesConfigurations
{
    public class ExpCategoryConfiguration : IEntityTypeConfiguration<ExpCategory>
    {
        public void Configure(EntityTypeBuilder<ExpCategory> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired();
            builder.Property(a => a.Category).IsRequired().HasMaxLength(50);
        }
    }
  
}
