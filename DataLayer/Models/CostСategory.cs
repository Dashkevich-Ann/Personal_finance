using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class CostСategory : BaseModel<int>
    {
        public string Name { get; set; }
        public decimal MonthLimit { get; set; }
        public virtual ICollection<Cost> Costs { get; set; }
    }
}
