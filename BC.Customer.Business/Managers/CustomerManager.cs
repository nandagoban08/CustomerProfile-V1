using BC.Customer.Business.Wrappers.Customer;
using BC.Customer.Contracts.Common;
using BC.Customer.Contracts.Managers;
using BC.Customer.Contracts.Repos;
using BC.Customer.Entities.Common;
using BC.Customer.Entities.DTO;
using BC.Customer.Entities.SearchParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.Customer.Business.Managers
{
    public class CustomerManager : ICustomerManager
    {
        private readonly IMapper<IList<Message>, ServiceResponse> _serviceResponseErrorMapper;
        private readonly IMapper<CustomerDataMapperWrapper, Customers> _customerDataMapper;
        private readonly IMapper<Object, ServiceResponse> _serviceResponseMapper;
        private readonly IValidator<CustomerDataMapperWrapper> _customerCreateValidator;
        private readonly ICustomerRepository _customerRepository;
        private readonly IBlobService _blobService;
        public CustomerManager( 
            IMapper<Object, ServiceResponse>  serviceResponseMapper, 
            IMapper<IList<Message>, ServiceResponse> serviceResponseErrorMapper,
            IMapper<CustomerDataMapperWrapper, Customers> customerDataMapper,
            IValidator<CustomerDataMapperWrapper> customerCreateValidator,
            ICustomerRepository customerRepository,
            IBlobService blobService
            )
        {
            _serviceResponseMapper = serviceResponseMapper;
            _serviceResponseErrorMapper = serviceResponseErrorMapper;
            _customerDataMapper = customerDataMapper;
            _customerCreateValidator = customerCreateValidator;
            _customerRepository = customerRepository;
            _blobService = blobService;

        }

        /// <summary>
        /// call Save and update customer repository object
        /// </summary>
        /// <param name="customerObject"></param>
        /// <returns></returns>
        public Task<ServiceResponse> SaveCustomer(Customers customerObject)
        {
            try
            {

                var customerDataMapperWrapper = new CustomerDataMapperWrapper { customer = customerObject };

                if (!_customerCreateValidator.Validate(customerDataMapperWrapper, out IList<Message> messages))
                {
                    return Task.FromResult(_serviceResponseErrorMapper.Map(messages));
                }

                var saveObject = _customerDataMapper.Map(customerDataMapperWrapper);

                if (saveObject.Id > 0)
                {
                    if(saveObject.File != null)
                    {
                        _blobService.DeleteFileBlobAsync(saveObject.ImageName);
                        var imageObject = _blobService.UploadProfilePicture(saveObject.File);
                        saveObject.ImageUrl = imageObject.Result.Item1;
                        saveObject.ImageName = imageObject.Result.Item2;
                    }
                    return Task.FromResult(_serviceResponseMapper.Map(_customerRepository.UpdateCustomer(saveObject)));
                }
                else
                {
                    var imageObject = _blobService.UploadProfilePicture(saveObject.File);
                    saveObject.ImageUrl = imageObject.Result.Item1;
                    saveObject.ImageName = imageObject.Result.Item2;
                    return Task.FromResult(_serviceResponseMapper.Map(_customerRepository.CreateCustomer(saveObject)));
                }
            }
            catch
            {
                throw ;
            }

        }

        /// <summary>
        /// call customer Search repository object 
        /// </summary>
        /// <param name="customerSearchParams"></param>
        /// <returns></returns>
        public ServiceResponse GetCustomersByNames(CustomerSearchParams customerSearchParams)
        {
            try
            {
                var returnObject = _customerRepository.GetCustomerBynames(customerSearchParams);
                return _serviceResponseMapper.Map(returnObject);
            }
            catch 
            {
                throw;
            }
        }
        /// <summary>
        ///  call customer delete repository object 
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public ServiceResponse DeleteCustomerById(Customers customer)
        {
            try
            {
                var returnObject = _customerRepository.DeleteCustomerById(customer);
                _blobService.DeleteFileBlobAsync(customer.ImageName);
                return _serviceResponseMapper.Map(returnObject);
            }
            catch 
            {
                throw;
            }
        }
    }
}
