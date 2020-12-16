using BC.Customer.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.Customer.Contracts.Common
{
    public interface IValidator<in T> : IDisposable
    {
        /// <summary>
        /// Validates the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="messages">The messages.</param>
        /// <returns>
        /// validation status
        /// </returns>
        bool Validate(T obj, out IList<Message> messages);

    }
}
