using System;
using System.Collections.Generic;
using System.Text;

namespace BC.Customer.Entities.DTO
{
    public class Base
    {
        public DateTime CreatedDate { get; set; } 
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; } 
        public string UpdatedBy { get; set; } 
    }
}
