using BC.Customer.Business.Wrappers.Customer;
using BC.Customer.Contracts.Common;
using BC.Customer.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.Customer.Business.Mappers.Customer
{
    public class CustomerDataMapper : IMapper<CustomerDataMapperWrapper, Customers>
    {
       
        public CustomerDataMapper()
        {
            
        }

        /// <summary>
        /// Map the customer object
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Customers Map(CustomerDataMapperWrapper input)
        {
            if (input.customer.Id > 0)
            {
                input.customer.UpdatedBy = "Nandagoban";
                input.customer.UpdatedDate = DateTime.Now;
            }
            else
            {
                input.customer.CreatedBy = "Nandagoban";
                input.customer.CreatedDate = DateTime.Now;
               
            }
            return input.customer;
        }
    }
}
