using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Entity;
using DataLayer.Models;
using DataLayer.Configuration;

namespace DataLayer{
    public class FinanceContext : DbContext
    {
        public FinanceContext(string connectionString) : base(connectionString)
        {
            Database.SetInitializer(new IntitializetContext());
        }

        public DbSet<Cost> Cost { get; set; }
        public DbSet<CostСategory> CostCategory { get; set; }
        public DbSet<Income> Income { get; set; }
        public DbSet<IncomeCategory> IncomeCategory { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CostConfiguration());
            modelBuilder.Configurations.Add(new CostCategoryConfiguration());
            modelBuilder.Configurations.Add(new IncomeConfiguration());
            modelBuilder.Configurations.Add(new IncomeCategoryConfiguration());
        }
    }

    internal class IntitializetContext : DropCreateDatabaseIfModelChanges<FinanceContext>
    {
        protected override void Seed(FinanceContext context)
        {
            context.SaveChanges();
        }
    }
}
