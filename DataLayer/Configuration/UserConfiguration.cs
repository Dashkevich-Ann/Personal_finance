using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Configuration
{
    class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            ToTable("Users").HasKey(b => b.Id);
            Property(b => b.Login).IsRequired().HasMaxLength(200);
            Property(b => b.Password).IsRequired().HasMaxLength(200);
            Property(b => b.FirstName).IsRequired().HasMaxLength(200);
            Property(b => b.SecondName).IsRequired().HasMaxLength(200);
        }
    }
}
