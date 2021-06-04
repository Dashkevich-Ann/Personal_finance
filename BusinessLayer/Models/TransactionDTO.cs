using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models
{
    public class TransactionDTO
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Comment { get; set; }
        public string DisplayAmount => Category?.Type == TransactionType.Income ? Amount.ToString("C") : "-" + Amount.ToString("C");
        public TransactionCategoryDTO Category { get; set; }
    }
}
