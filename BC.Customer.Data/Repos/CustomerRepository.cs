using BC.Customer.Contracts.Common;
using BC.Customer.Data.CustomerDataModels;
using BC.Customer.Data.CustomerDbContext;
using BC.Customer.Entities.DTO;
using System;
using System.Transactions;
using BC.Customer.Contracts.Repos;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using BC.Customer.Entities.SearchParams;

namespace BC.Customer.Data.Repos
{
    public class CustomerRepository : ICustomerRepository
    {
        /// <summary>
        /// The ims entities
        /// </summary>
        private readonly CustomerEntities _customerEntities;

        /// <summary>
        /// The entity mapper
        /// </summary>
        private readonly IEntityMapper _entityMapper;

        public  CustomerRepository(CustomerEntities customerEntities, IEntityMapper entityMapper)
       {
            _customerEntities = customerEntities;
            _entityMapper = entityMapper;
       }

        /// <summary>
        /// Create the customer.
        /// </summary>
        /// <param name="saveObject">The save object.</param>
        /// <returns></returns>
        public Customers CreateCustomer(Customers saveObject)
        {
            try
            {
                    #region Save Customer User
                    var customerToSave = _entityMapper.Map<Customers, BcTCustomer>(saveObject);
                    var customerSaved = _customerEntities.BcTCustomers.Add(customerToSave).Entity;
                    _customerEntities.SaveChanges();
                    #endregion
                    return _entityMapper.Map<BcTCustomer, Customers>(customerSaved);
            }
            catch 
            {       
                throw;
            }
        }

        /// <summary>
        /// Update customer
        /// </summary>
        /// <param name="saveObject"></param>
        /// <returns></returns>
        public Customers UpdateCustomer(Customers saveObject)
        {
            try
            {
                var customerUpdate = _customerEntities.BcTCustomers.FirstOrDefault(a => a.Id == saveObject.Id);
                if (customerUpdate != null)
                {
                    customerUpdate.FirstName = saveObject.FirstName;
                    customerUpdate.LastName = saveObject.LastName;
                    customerUpdate.Address = saveObject.Address;
                    customerUpdate.PostalCode = saveObject.PostalCode;
                    customerUpdate.Phone = saveObject.Phone;
                    customerUpdate.ImageUrl = saveObject.ImageUrl;
                    customerUpdate.ImageName = saveObject.ImageName;
                    customerUpdate.UpdatedBy = saveObject.UpdatedBy;
                    customerUpdate.UpdatedDate = saveObject.UpdatedDate;
                    _customerEntities.SaveChanges();
                }
                return _entityMapper.Map<BcTCustomer, Customers>(customerUpdate);
            }
            catch 
            {  
                throw;
            }
        }

        /// <summary>
        /// Get the customers 
        /// </summary>
        /// <param name="customerSearchParams"></param>
        /// <returns></returns>
        public List<Customers> GetCustomerBynames(CustomerSearchParams customerSearchParams)
        {
            try
            {
                var returnObj = _customerEntities.BcTCustomers.Where((l => (l.FirstName.Contains(customerSearchParams.FirstName) || customerSearchParams.FirstName == null || customerSearchParams.FirstName == "") 
                && (l.LastName.Contains(customerSearchParams.LastName) || customerSearchParams.LastName == null || customerSearchParams.LastName == "")
                && (l.Id == customerSearchParams.Id || customerSearchParams.Id == null || customerSearchParams.Id == 0))).ToList() ;
                return _entityMapper.Map<List<BcTCustomer>, List<Customers>>(returnObj);
            }
            catch (Exception ex)
            {
                //_logger.LogError<ImsUser>(ex.ToString());
                throw;
            }
        }

        /// <summary>
        /// delete the customer
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool DeleteCustomerById(Customers request)
        {
            try
            {
                var Customer = new BcTCustomer()
                {
                    Id = request.Id
                };
                var returnObj = _customerEntities.BcTCustomers.Remove(Customer);
                _customerEntities.SaveChanges();             
                return true;
            }
            catch 
            {
               
                throw;
            }
        }
    }
}
