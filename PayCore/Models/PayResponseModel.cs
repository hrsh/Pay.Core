// ***********************************************************************
// Assembly         : PayCore
// Author           : Mazdak Shojaie
// Created          : 09-24-2019
//
// Last Modified By : Mazdak Shojaie
// Last Modified On : 09-24-2019
// ***********************************************************************
// <copyright file="PayResponseModel.cs" company="PayCore">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using PayCore.Config;

namespace PayCore.Models {
    /// <summary>
    /// Class PayResponseModel.
    /// </summary>
    public class PayResponseModel {
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        [JsonProperty ("status")]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        /// <value>The token.</value>
        [JsonProperty ("token")]
        public string Token { get; set; }

        /// <summary>
        /// Gets the payment URL.
        /// </summary>
        /// <value>The payment URL.</value>
        public string PaymentUrl => !string.IsNullOrWhiteSpace (Token) && Status == "1" ?
            string.Format (PayUrls.PaymentUrl, Token) :
            "";
    }
}