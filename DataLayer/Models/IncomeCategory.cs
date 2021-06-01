using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class IncomeCategory : BaseModel<int>
    {
        public string Name { get; set; }
        public virtual ICollection<Income> Incomes { get; set; }
    }
}
