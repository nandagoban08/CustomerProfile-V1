 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.Customer.Entities.Common
{
    public class ServiceResponse
    {
        /// <summary>
        /// Gets or sets the return object.
        /// </summary>
        /// <value>
        /// The return object.
        /// </value>
        public Object ReturnObject { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is error.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is error; otherwise, <c>false</c>.
        /// </value>
        public bool IsError { get; set; }

        /// <summary>
        /// Gets or sets the messages.
        /// </summary>
        /// <value>
        /// The messages.
        /// </value>
        public IList<Message> Messages { get; set; }
    }
}
