using BusinessLayer.Models;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Mappers
{
    public static class TransactionMapper
    {
        public static TransactionDTO MapToDTO(this Cost cost)
        {
            var transactionDTO = new TransactionDTO
            {
                Id = cost.Id,
                Date = cost.Date,
                Amount = cost.Amount,
                Comment = cost.Comment,
                Category = cost.CostCategory.MapToDTO()
            };
            return transactionDTO;
        }

        public static TransactionDTO MapToDTO(this Income income)
        {
            var transactionDTO = new TransactionDTO
            {
                Id = income.Id,
                Date = income.Date,
                Amount = income.Amount,
                Comment = income.Comment,
                Category = income.IncomeCategory.MapToDTO()
            };
            return transactionDTO;
        }


        public static TransactionCategoryDTO MapToDTO(this CostСategory costСategory)
        {
            return new TransactionCategoryDTO
            {
                Id = costСategory.Id,
                Name = costСategory.Name,
                MonthLimit = costСategory.MonthLimit,
                Type = TransactionType.Cost
            };
        }

        public static TransactionCategoryDTO MapToDTO(this IncomeCategory incomeCategory)
        {
            return new TransactionCategoryDTO
            {
                Id = incomeCategory.Id,
                Name = incomeCategory.Name,
                Type = TransactionType.Income
            };
        }
    }
}
