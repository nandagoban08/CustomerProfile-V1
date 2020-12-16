using System;
using System.Collections.Generic;
using System.Text;

namespace BC.Customer.Contracts.Common
{
    public interface IEntityMapper
    {
        /// <summary>
        /// Maps the specified source.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TDestination">The type of the destination.</typeparam>
        /// <param name="source">The source.</param>
        TDestination Map<TSource, TDestination>(TSource source);
    }
}
