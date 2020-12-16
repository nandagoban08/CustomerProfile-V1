using System;
using System.Collections.Generic;

#nullable disable

namespace BC.Customer.Data.CustomerDataModels
{
    public partial class BcTCustomer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public int Phone { get; set; }
        public string ImageUrl { get; set; }
        public string ImageName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}
