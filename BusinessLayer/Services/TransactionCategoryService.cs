using BusinessLayer.Mappers;
using BusinessLayer.Models;
using DataLayer.Interfaces;
using DataLayer.Models;
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

        public void DeleteCategory(TransactionCategoryDTO categoryDTO)
        {
            if (categoryDTO == null)
            {
                throw new NullReferenceException("Transaction is not found.");
            }
            if (categoryDTO.Type == TransactionType.Cost)
            {
                using (unitOfWork = new UnitOfWork())
                {
                    var cost = unitOfWork.CostCategoryRepository.Find(x => x.Id == categoryDTO.Id).FirstOrDefault();
                    unitOfWork.CostCategoryRepository.Delete(cost);
                    unitOfWork.Commit();
                }
            }
            else if (categoryDTO.Type == TransactionType.Income)
            {
                using (unitOfWork = new UnitOfWork())
                {
                    var income = unitOfWork.IncomeCategoryRepository.Find(x => x.Id == categoryDTO.Id).FirstOrDefault();
                    unitOfWork.IncomeCategoryRepository.Delete(income);
                    unitOfWork.Commit();
                }
            }
        }

        public bool CategoryInUse(TransactionCategoryDTO categoryDTO)
        {
            if (categoryDTO == null)
            {
                throw new NullReferenceException("Transaction is not found.");
            }
            using (unitOfWork = new UnitOfWork())
            {
                if (categoryDTO.Type == TransactionType.Cost)
                {
                    return unitOfWork.CostRepository.GetAll().Any(x => x.CostCategoryId == categoryDTO.Id);
                }
                else if (categoryDTO.Type == TransactionType.Income)
                {
                    return unitOfWork.IncomeRepository.GetAll().Any(x => x.IncomeCategoryId == categoryDTO.Id);
                }
            }
            return false;
        }

        public void CreateCategory(TransactionCategoryDTO transactionCategoryDTO)
        {
            using(unitOfWork = new UnitOfWork())
            {
                if( transactionCategoryDTO.Type == TransactionType.Cost)
                {
                    var cost = new CostСategory
                    {
                        Name = transactionCategoryDTO.Name,
                        MonthLimit = transactionCategoryDTO.MonthLimit.GetValueOrDefault()
                    };
                    unitOfWork.CostCategoryRepository.Create(cost);
                }
                else if(transactionCategoryDTO.Type == TransactionType.Income)
                {
                    var income = new IncomeCategory
                    {
                        Name = transactionCategoryDTO.Name
                    };
                    unitOfWork.IncomeCategoryRepository.Create(income);
                }
                unitOfWork.Commit();
            }
        }

        public void EditCategory(TransactionCategoryDTO transactionCategoryDTO)
        {
            using(unitOfWork = new UnitOfWork())
            {
                if(transactionCategoryDTO.Type == TransactionType.Income)
                {
                    var income = unitOfWork.IncomeCategoryRepository.Find(x => x.Id == transactionCategoryDTO.Id).First();
                    income.Name = transactionCategoryDTO.Name;
                }
                else if(transactionCategoryDTO.Type == TransactionType.Cost)
                {
                    var cost = unitOfWork.CostCategoryRepository.Find(x => x.Id == transactionCategoryDTO.Id).First();
                    cost.Name = transactionCategoryDTO.Name;
                    cost.MonthLimit = transactionCategoryDTO.MonthLimit.GetValueOrDefault();
                }
                unitOfWork.Commit();
            }
        }
    }
}
