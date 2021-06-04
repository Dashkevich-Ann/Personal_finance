using BusinessLayer.Mappers;
using BusinessLayer.Models;
using DataLayer.Interfaces;
using DataLayer.Models;
using DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class TransactionService
    {
        private IUnitOfWork _unit;
        public TransactionService()
        {
        }

        public IEnumerable<TransactionDTO> GetAllTransactions() 
        {
            using (_unit = new UnitOfWork())
            {
                var costs = _unit.CostRepository.GetAll().Include(x => x.CostCategory).ToList();
                var incomes = _unit.IncomeRepository.GetAll().Include(x => x.IncomeCategory).ToList();

                var costDTOs = costs.Select(x => x.MapToDTO());
                var incomeDTOs = incomes.Select(x => x.MapToDTO());

                return costDTOs.Concat(incomeDTOs).OrderBy(x => x.Date);
            }          
        }

        public void CreateTransaction(TransactionDTO transactionDTO)
        {
            using (_unit = new UnitOfWork())
            {
                if (transactionDTO.Category.Type == TransactionType.Income)
                {
                    var income = new Income
                    {
                        Id = Guid.NewGuid(),
                        Amount = transactionDTO.Amount,
                        Comment = transactionDTO.Comment,
                        Date = transactionDTO.Date,
                        IncomeCategoryId = transactionDTO.Category.Id
                    };

                    _unit.IncomeRepository.Create(income);
                }
                else if (transactionDTO.Category.Type == TransactionType.Cost)
                {
                    var cost = new Cost
                    {
                        Id = Guid.NewGuid(),
                        Amount = transactionDTO.Amount,
                        Comment = transactionDTO.Comment,
                        Date = transactionDTO.Date,
                        CostCategoryId = transactionDTO.Category.Id,
                    };
                    _unit.CostRepository.Create(cost);
                }
                _unit.Commit();
            }
        }

        public void EditTransaction(TransactionDTO transactionDTO)
        {
            using(_unit = new UnitOfWork())
            {
                if (transactionDTO.Category.Type == TransactionType.Income)
                {
                    var income = _unit.IncomeRepository.Find(x => x.Id == transactionDTO.Id).First();
                    income.Amount = transactionDTO.Amount;
                    income.Comment = transactionDTO.Comment;
                    income.Date = transactionDTO.Date;
                    income.IncomeCategoryId = transactionDTO.Category.Id;
                }
                else if (transactionDTO.Category.Type == TransactionType.Cost)
                {
                    var cost = _unit.CostRepository.Find(x => x.Id == transactionDTO.Id).First();
                    cost.Amount = transactionDTO.Amount;
                    cost.Comment = transactionDTO.Comment;
                    cost.Date = transactionDTO.Date;
                    cost.CostCategoryId = transactionDTO.Category.Id;
                }
                _unit.Commit();
            }
        }

        public void DeleteTransaction(TransactionDTO transactionDTO)
        {
            if (transactionDTO == null)
            {
                throw new NullReferenceException("Transaction is not found.");
            }
            if (transactionDTO.Category.Type == TransactionType.Cost)
            {
                using (_unit = new UnitOfWork())
                {
                    var cost = _unit.CostRepository.Find(x => x.Id == transactionDTO.Id).FirstOrDefault();
                    _unit.CostRepository.Delete(cost);
                    _unit.Commit();
                }
            }
            else if (transactionDTO.Category.Type == TransactionType.Income)
            {
                using (_unit = new UnitOfWork())
                {
                    var income = _unit.IncomeRepository.Find(x => x.Id == transactionDTO.Id).FirstOrDefault();
                    _unit.IncomeRepository.Delete(income);
                    _unit.Commit();
                }
            }
        } 
    }
}
