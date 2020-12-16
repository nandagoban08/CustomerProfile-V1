using BC.Customer.Entities.Common;
using BC.Customer.Entities.DTO;
using BC.Customer.Entities.SearchParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.Customer.Contracts.Managers
{
    public interface ICustomerManager
    {
        Task<ServiceResponse> SaveCustomer(Customers saveObject);
        ServiceResponse GetCustomersByNames(CustomerSearchParams customerSearchParams);
        ServiceResponse DeleteCustomerById(Customers request);

    }
}
