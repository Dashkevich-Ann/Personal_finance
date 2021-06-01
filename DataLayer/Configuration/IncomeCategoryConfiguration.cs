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
    internal class IncomeCategoryConfiguration : EntityTypeConfiguration<IncomeCategory>
    {
        public IncomeCategoryConfiguration()
        {
            ToTable("IncomeCategories").HasKey(b => b.Id);
            Property(b => b.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(b => b.Name).IsRequired().HasMaxLength(200);
        }
    }
}
