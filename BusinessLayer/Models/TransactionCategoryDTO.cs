using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models
{
    public class TransactionCategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? MonthLimit { get; set; }
        public TransactionType Type { get; set; }
        public string LimitDisplayValue => Type == TransactionType.Income ? "-" : MonthLimit?.ToString("C") ?? "-";
    }

    public enum TransactionType
    {
        Income = 0,
        Cost = 1
    }
}
