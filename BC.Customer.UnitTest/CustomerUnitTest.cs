using BC.Customer.Contracts.Common;
using BC.Customer.Contracts.Managers;
using BC.Customer.Contracts.Repos;
using BC.Customer.Entities.Common;
using BC.Customer.Entities.DTO;
using BC.Customer.Entities.SearchParams;
using BC.Customer.WebApi.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BC.Customer.UnitTest
{
    public class CustomerUnitTest
    {

        private readonly Mock<IMapper<Object, ServiceResponse>> _serviceResponseMapper = new Mock<IMapper<object, ServiceResponse>>();

        private readonly Mock<IMapper<IList<Message>, ServiceResponse>> _serviceResponseErrorMapper = new Mock<IMapper<IList<Message>, ServiceResponse>>();

        private ServiceResponse _responseObj;

        private ServiceResponse GetTestCountrys(CustomerSearchParams test)
        {
            var _country = new List<Customers>() {
                new Customers{
                    Id = 1,FirstName = "Nandagoban", LastName = "Subramaniam", Address="Singapore", ImageName="", ImageUrl="", Phone =07775623,PostalCode="652589",File= null,UpdatedBy="Nandagoban",UpdatedDate=DateTime.Now, CreatedDate=DateTime.Now,CreatedBy="Nandagoban"
                }
            };
            _country = _country.FindAll(a => a.FirstName == test.FirstName || test.FirstName == null || test.FirstName == "").ToList();
            _responseObj = new ServiceResponse
            {
                ReturnObject = _country
            };
            return _responseObj;
        }

        [Fact]
        public async Task GetCustomer()
        {
            var mockLogger = new Moq.Mock<ICustomerManager>();
            CustomerSearchParams searchParams = new CustomerSearchParams();
            searchParams.FirstName = "Nandagoban";
            searchParams.LastName = "";
            mockLogger.Setup(m => m.GetCustomersByNames(searchParams)).Returns(GetTestCountrys(searchParams));
            var controller = new CustomerController(mockLogger.Object, null, null, null);
            var actualResult =  controller.GetCustomers(searchParams);
            var expectedResult =  GetExpectedResult();
            var obj1 = (List <Customers>)expectedResult.ReturnObject;
            var obj2 = (List<Customers>)actualResult.ReturnObject;
            Assert.Equal(obj1[0].Id, obj2[0].Id);
            Assert.Equal(obj1[0].FirstName, obj2[0].FirstName);

        }

        [Fact]
        public async Task GetCustomerNotExist()
        {
            var mockLogger = new Moq.Mock<ICustomerManager>();
            CustomerSearchParams searchParams = new CustomerSearchParams();
            searchParams.FirstName = "Test";
            searchParams.LastName = "";
            mockLogger.Setup(m => m.GetCustomersByNames(searchParams)).Returns(GetTestCountrys(searchParams));
            var controller = new CustomerController(mockLogger.Object, null, null, null);
            var actualResult = controller.GetCustomers(searchParams);
            var expectedResult = GetNotExistExpectedResult();
            var obj1 = (List<Customers>)expectedResult.ReturnObject;
            var obj2 = (List<Customers>)actualResult.ReturnObject;
            Assert.Equal(obj1.Count(), obj1.Count());
        }

        public ServiceResponse GetExpectedResult()
        {
                var _customer = new List<Customers>() {
                new Customers{
                      Id = 1,FirstName = "Nandagoban", LastName = "Subramaniam", Address="Singapore", ImageName="", ImageUrl="", Phone =07775623,PostalCode="652589",File= null,UpdatedBy="Nandagoban",UpdatedDate=DateTime.Now, CreatedDate=DateTime.Now,CreatedBy="Nandagoban"
                }

            };

            _responseObj = new ServiceResponse
            {
                ReturnObject = _customer
            };
            return _responseObj;
        }

        public ServiceResponse GetNotExistExpectedResult()
        {
            var _customer = new List<Customers>() {
              
            };

            _responseObj = new ServiceResponse
            {
                ReturnObject = _customer
            };
            return _responseObj;
        }

    }
}
