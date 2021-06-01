using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public abstract class BaseModel
    {
    }

    public class BaseModel<T> : BaseModel where T: struct
    {
        public T Id { get; set; }
    }
}
