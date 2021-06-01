using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Configuration
{
    internal class CostConfiguration : EntityTypeConfiguration<Cost>
    {
        public CostConfiguration()
        {
            ToTable("Costs").HasKey(b => b.Id);
            Property(b => b.Date).IsRequired();
            Property(b => b.Amount).IsRequired().HasPrecision(6,2);
            Property(b => b.Comment).IsOptional().HasMaxLength(1000);

            HasRequired(x => x.CostCategory)
               .WithMany(u => u.Costs)
               .HasForeignKey(x => x.CostCategoryId)
               .WillCascadeOnDelete(true);
        }
    }
}
