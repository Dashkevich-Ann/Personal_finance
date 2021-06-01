using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Cost> CostRepository { get; }
        IRepository<CostСategory> CostCategoryRepository { get; }
        IRepository<Income> IncomeRepository { get; }
        IRepository<IncomeCategory> IncomeCategoryRepository { get; }
        void Commit();
    }
}
