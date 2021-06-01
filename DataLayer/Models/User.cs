using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class User : BaseModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public virtual ICollection<Cost> Costs { get; set; }
        public virtual ICollection<Income> Incomes { get; set; }

    }
}
