// ***********************************************************************
// Assembly         : PayCore
// Author           : Mazdak Shojaie
// Created          : 09-24-2019
//
// Last Modified By : Mazdak Shojaie
// Last Modified On : 09-24-2019
// ***********************************************************************
// <copyright file="PayHelperExtension.cs" company="PayCore">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.Extensions.DependencyInjection;

namespace PayCore.Helper {
    /// <summary>
    /// Class PayHelperExtension.
    /// </summary>
    public static class PayHelperExtension {
        /// <summary>
        /// Adds the <see cref="PayProvider"/> to service collection.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <returns>IServiceCollection.</returns>
        public static IServiceCollection AddPay (this IServiceCollection service) {
            service.AddHttpClient<IPayProvider, PayProvider> ();
            return service;
        }
    }
}