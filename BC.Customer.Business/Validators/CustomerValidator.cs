using BC.Customer.Business.Wrappers.Customer;
using BC.Customer.Contracts.Common;
using BC.Customer.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.Customer.Business.Validators
{
    public class CustomerValidator : IValidator<CustomerDataMapperWrapper>
    {
        private readonly IErrorMessagesHandler _commonErrorHandler;
        public CustomerValidator(IErrorMessagesHandler commonErrorHandler)
        {
            _commonErrorHandler = commonErrorHandler;
        }
       
        /// <summary>
        /// Validate customer object
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="messages"></param>
        /// <returns></returns>
        public bool Validate(CustomerDataMapperWrapper obj, out IList<Message> messages)
        {
            messages = new List<Message>();

            if (string.IsNullOrEmpty(obj.customer.FirstName))
            {
                messages.Add(_commonErrorHandler.GetFirstNameCantbeEmpty());
                return false;
            }
            if (string.IsNullOrEmpty(obj.customer.LastName))
            {
                messages.Add(_commonErrorHandler.GetLastNameCantbeEmpty());
                return false;
            }
            if (obj.customer.Phone.ToString().Length > 9)
            {
                messages.Add(_commonErrorHandler.GetPhoneNumberRange());
                return false;
            }
            if (string.IsNullOrEmpty(obj.customer.Address))
            {
                messages.Add(_commonErrorHandler.GetAddressCantbeEmpty());
                return false;
            }

            if (obj.customer.File != null &&
                ((obj.customer.File.ContentType != "image/jpeg") && (obj.customer.File.ContentType != "image/png")))
            {
                messages.Add(_commonErrorHandler.GetInvaildImageFormat());       
                return false;
            }
            if (obj.customer.File == null && obj.customer.Id == 0)
            {
                messages.Add(_commonErrorHandler.GetProfileCantbeEmpty());
                return false;
            }


            if (obj.customer.Phone == 0)
            {
                messages.Add(_commonErrorHandler.GetPhoneCantbeEmpty());
                return false;
            }
            if (string.IsNullOrEmpty(obj.customer.PostalCode))
            {
                messages.Add(_commonErrorHandler.GetPostalCodeCantbeEmpty());
                return false;
            }
         

            return true;
        }

       
        protected virtual void Dispose(bool disposing)
        {
        }

    
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~CustomerValidator()
        {
            Dispose(false);
        }
    }
}
