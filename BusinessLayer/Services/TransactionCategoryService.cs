using BusinessLayer.Mappers;
using BusinessLayer.Models;
using DataLayer.Interfaces;
using DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class TransactionCategoryService
    {
        private IUnitOfWork unitOfWork;
        public TransactionCategoryService()
        {
           
        }

        public IEnumerable<TransactionCategoryDTO> GetAllCategories()
        {
            using (unitOfWork = new UnitOfWork())
            {
                var costs = unitOfWork.CostCategoryRepository.GetAll().ToList();
                var incomes = unitOfWork.IncomeCategoryRepository.GetAll().ToList();

                var costDTOs = costs.Select(x => x.MapToDTO());
                var incomeDTOs = incomes.Select(x => x.MapToDTO());

                return costDTOs.Concat(incomeDTOs).OrderBy(n => n.Name);
            }
        }
    }
}
