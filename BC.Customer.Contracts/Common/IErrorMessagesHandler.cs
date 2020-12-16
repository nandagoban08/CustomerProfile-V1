using BC.Customer.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.Customer.Contracts.Common
{
    /// <summary>
    /// ErrorMessagesHandler Contract
    /// </summary>
    public interface IErrorMessagesHandler
    {
        Message GetFirstNameCantbeEmpty();
        Message GetLastNameCantbeEmpty();
        Message GetServiceError();
        Message GetAddressCantbeEmpty();
        Message GetPhoneCantbeEmpty();
        Message GetProfileCantbeEmpty();
        Message GetPostalCodeCantbeEmpty();
        Message GetPhoneNumberRange();
        Message GetInvaildImageFormat();

    }
}
