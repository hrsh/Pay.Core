// ***********************************************************************
// Assembly         : PayCore
// Author           : Mazdak Shojaie
// Created          : 09-24-2019
//
// Last Modified By : Mazdak Shojaie
// Last Modified On : 09-24-2019
// ***********************************************************************
// <copyright file="IPayProvider.cs" company="PayCore">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Threading.Tasks;
using PayCore.Helper;
using PayCore.Models;

namespace PayCore {
    /// <summary>
    /// Interface IPayProvider
    /// </summary>
    public interface IPayProvider {
        /// <summary>
        /// Sends payment request asynchronous.
        /// </summary>
        /// <param name="model">The payment model <see cref="PayCore.Models.PayRequestModel"/>.</param>
        /// <returns>Task&lt;PayResult&lt;PayResponseModel&gt;&gt;.</returns>
        Task<PayResult<PayResponseModel>> AuthorizeAsync (PayRequestModel model);

        /// <summary>
        /// Verifies the payment result asynchronous.
        /// </summary>
        /// <param name="model">The model <see cref="PayCore.Models.VerifyRequestModel"/>.</param>
        /// <returns>Task&lt;PayResult&lt;VerifyResponseModel&gt;&gt;.</returns>
        Task<PayResult<VerifyResponseModel>> VerifyAsync (VerifyRequestModel model);
    }
}