using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models
{
    public class TransactionStatisticsDTO
    {
        public decimal CostSum { get; set; }
        public decimal IncomeSum { get; set; }
        public IEnumerable<CostStatisticItem> CostStatistics { get; set; }
            
    }

    public class CostStatisticItem
    {
        public TransactionCategoryDTO Category { get; set; }
        public decimal Sum { get; set; }
    }
}
