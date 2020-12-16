using BC.Customer.Common;
using BC.Customer.Contracts.Common;
using BC.Customer.Contracts.Managers;
using BC.Customer.Entities.Common;
using BC.Customer.Entities.DTO;
using BC.Customer.Entities.SearchParams;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BC.Customer.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerManager _customerManager;
        private readonly IMapper<IList<Message>, ServiceResponse> _serviceResponseErrorMapper;
        private readonly IErrorMessagesHandler _errorMessagesHandler;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(ICustomerManager customerManager, 
            IMapper<IList<Message>, ServiceResponse> serviceResponseErrorMapper,
            IErrorMessagesHandler errorMessagesHandler, ILogger<CustomerController> logger)
        {
            _customerManager = customerManager;
            _serviceResponseErrorMapper = serviceResponseErrorMapper;
            _errorMessagesHandler = errorMessagesHandler;
            _logger = logger;


        }

        [HttpPost("SaveCustomer")]
        public Task<ServiceResponse> SaveCustomer([FromForm] Customers request)
        {
            try
            {
                return _customerManager.SaveCustomer(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return Task.FromResult(_serviceResponseErrorMapper.Map(new List<Message> { _errorMessagesHandler.GetServiceError() }));
            }
        }

        [HttpPost("getcustomers")]
        public ServiceResponse GetCustomers([FromBody] CustomerSearchParams customerSearchParams)
        {
            try
            {
                return _customerManager.GetCustomersByNames(customerSearchParams);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return _serviceResponseErrorMapper.Map(new List<Message> { _errorMessagesHandler.GetServiceError() });
            }
        }

        [HttpPost("deletecustomer")]
        public ServiceResponse DeleteCustomer([FromBody] Customers request)
        {
            try
            {
                return _customerManager.DeleteCustomerById(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return _serviceResponseErrorMapper.Map(new List<Message> { _errorMessagesHandler.GetServiceError() });
            }
        }
    }
}
