using DataLayer.Interfaces;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FinanceContext _dbContext;
        private IRepository<Cost> _costRepository;
        private IRepository<CostСategory> _costCategoryRepository;
        private IRepository<Income> _incomeRepository;
        private IRepository<IncomeCategory> _incomeCategoryRepository;
        private bool _disposed;

        public IRepository<Cost> CostRepository => _costRepository ?? (_costRepository = new Repository<Cost>(_dbContext));

        public IRepository<CostСategory> CostCategoryRepository => _costCategoryRepository ?? (_costCategoryRepository = new Repository<CostСategory>(_dbContext));

        public IRepository<Income> IncomeRepository => _incomeRepository ?? (_incomeRepository = new Repository<Income>(_dbContext));

        public IRepository<IncomeCategory> IncomeCategoryRepository => _incomeCategoryRepository ?? (_incomeCategoryRepository = new Repository<IncomeCategory>(_dbContext));

        public UnitOfWork()
        {
            _dbContext = new FinanceContext("FinanceConnection");
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                _dbContext.Dispose();
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
