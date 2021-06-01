using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class Cost : BaseModel<Guid>
    {
        public int CostCategoryId { get; set; }
        public CostСategory CostCategory { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Comment { get; set; }
    }
}
