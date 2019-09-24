// ***********************************************************************
// Assembly         : PayCore
// Author           : Mazdak Shojaie
// Created          : 09-24-2019
//
// Last Modified By : Mazdak Shojaie
// Last Modified On : 09-24-2019
// ***********************************************************************
// <copyright file="PayUrls.cs" company="PayCore">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace PayCore.Config {
    /// <summary>
    /// Class PayUrls.
    /// </summary>
    public static class PayUrls {
        /// <summary>
        /// The authorize(send) URL
        /// </summary>
        public const string AuthorizeUrl = "https://pay.ir/pg/send";

        /// <summary>
        /// The payment URL
        /// </summary>
        public const string PaymentUrl = "https://pay.ir/pg/{0}";

        /// <summary>
        /// The verify URL
        /// </summary>
        public const string VerifyUrl = "https://pay.ir/pg/verify";
    }
}