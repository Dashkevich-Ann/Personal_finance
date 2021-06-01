using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class Income : BaseModel<Guid>
    {
        public int IncomeCategoryId { get; set; }
        public IncomeCategory IncomeCategory { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Comment { get; set; }
    }
}
