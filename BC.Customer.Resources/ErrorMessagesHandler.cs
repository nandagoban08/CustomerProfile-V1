using BC.Customer.Contracts.Common;
using BC.Customer.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BC.Customer.Resources
{
    public class ErrorMessagesHandler : IErrorMessagesHandler
    {

      
        /// <summary>
        /// Gets the first name can not empty.
        /// </summary>
        /// <returns></returns>
        public Message GetFirstNameCantbeEmpty()
        {
            return new Message
            {
                Code = "E0001",
                Description = Error.E0001
            };
        }

        /// <summary>
        ///Gets the Last name can not empty.
        /// </summary>
        /// <returns></returns>
        public Message GetLastNameCantbeEmpty()
        {
            return new Message
            {
                Code = "E0002",
                Description = Error.E0002
            };
        }

        /// <summary>
        /// Gets the service error.
        /// </summary>
        /// <returns></returns>
        public Message GetServiceError()
        {
            return new Message
            {
                Code = "E0003",
                Description = Error.E0003
            };
        }

        /// <summary>
        /// Gets the Address can not empty 
        /// </summary>
        /// <returns></returns>
        public Message GetAddressCantbeEmpty()
        {
            return new Message
            {
                Code = "E0004",
                Description = Error.E0004
            };
        }

        /// <summary>
        /// Gets the Phone can not empty
        /// </summary>
        /// <returns></returns>
        public Message GetPhoneCantbeEmpty()
        {
            return new Message
            {
                Code = "E0006",
                Description = Error.E0006
            };
        }

        /// <summary>
        /// Gets the profile cant not be empty
        /// </summary>
        /// <returns></returns>
        public Message GetProfileCantbeEmpty()
        {
            return new Message
            {
                Code = "E0007",
                Description = Error.E0007
            };
        }

        /// <summary>
        /// Gets postal code can not be empty
        /// </summary>
        /// <returns></returns>
        public Message GetPostalCodeCantbeEmpty()
        {
            return new Message
            {
                Code = "E0005",
                Description = Error.E0005
            };
        }

        /// <summary>
        /// gets the phone number can not empty
        /// </summary>
        /// <returns></returns>
        public Message GetPhoneNumberRange()
        {
            return new Message
            {
                Code = "E0008",
                Description = Error.E0008
            };
        }

        /// <summary>
        /// gets the Invaild image format 
        /// </summary>
        /// <returns></returns>
        public Message GetInvaildImageFormat()
        {
            return new Message
            {
                Code = "E0009",
                Description = Error.E0009
            };
        }
      





    }
}
