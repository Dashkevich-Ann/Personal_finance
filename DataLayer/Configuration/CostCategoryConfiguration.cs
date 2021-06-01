using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Configuration
{
    internal class CostCategoryConfiguration : EntityTypeConfiguration<CostСategory>
    {
        public CostCategoryConfiguration()
        {
            ToTable("CostCategories").HasKey(b => b.Id);
            Property(b => b.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(b => b.Name).IsRequired().HasMaxLength(200);
            Property(b => b.MonthLimit).IsRequired().HasPrecision(6, 2);
        }
    }
}
