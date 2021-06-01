using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interfaces
{
    public interface IRepository<T> where T : BaseModel
    {
        IQueryable<T> GetAll();
        T Get(int id);
        T Get(Expression<Func<T, bool>> predicate);
        void Create(T item);
        void Delete(T item);
        IQueryable<T> Find(Expression<Func<T, bool>> predicate);
        int Count(Expression<Func<T, bool>> predicate = null);
    }
}
