using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.Customer.Entities.DTO
{
    public class Customers : Base
    {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public int Phone { get; set; }
        public string ImageName { get; set; }
        public string ImageUrl { get; set; }

        [NotMapped]
        public IFormFile? File { get; set; }

    }
}
