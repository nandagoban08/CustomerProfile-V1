using BC.Customer.Entities.DTO;
using BC.Customer.Entities.SearchParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.Customer.Contracts.Repos
{
    public interface ICustomerRepository
    {
        Customers CreateCustomer(Customers saveObject);

        Customers UpdateCustomer(Customers saveObject);

        List<Customers> GetCustomerBynames(CustomerSearchParams customerSearchParams);

        bool DeleteCustomerById(Customers request);
    }
}
