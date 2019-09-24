// ***********************************************************************
// Assembly         : PayCore
// Author           : Mazdak Shojaie
// Created          : 09-24-2019
//
// Last Modified By : Mazdak Shojaie
// Last Modified On : 09-24-2019
// ***********************************************************************
// <copyright file="PayRequestModel.cs" company="PayCore">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using PayCore.Config;

namespace PayCore.Models {
    /// <summary>
    /// Class PayRequestModel.
    /// </summary>
    public class PayRequestModel {
        /// <summary>
        /// Gets or sets the API.
        /// </summary>
        /// <value>The API.</value>
        [JsonProperty ("api")]
        internal string Api { get; set; }

        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        /// <value>The amount.</value>
        [JsonProperty ("amount")]
        public double Amount { get; set; }

        /// <summary>
        /// Gets or sets the callback URL.
        /// </summary>
        /// <value>The callback URL.</value>
        [JsonProperty ("redirect")]
        public string CallbackUrl { get; set; }

        /// <summary>
        /// Gets or sets the mobile.
        /// </summary>
        /// <value>The mobile.</value>
        [JsonProperty ("mobile")]
        public string Mobile { get; set; }

        /// <summary>
        /// Gets or sets the factor number.
        /// </summary>
        /// <value>The factor number.</value>
        [JsonProperty ("factorNumber")]
        public string InvoiceNumber { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [JsonProperty ("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the valid card number.
        /// </summary>
        /// <value>The valid card number.</value>
        [JsonProperty ("validCardNumber")]
        public string ValidCardNumber { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PayRequestModel"/> class.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <param name="callbackUrl">The callback URL.</param>
        public PayRequestModel (double amount, string callbackUrl) {
            Amount = amount;
            CallbackUrl = callbackUrl;
        }
    }

    /// <summary>
    /// Class PayRequestModelExtension.
    /// </summary>
    internal static class PayRequestModelExtension {
        /// <summary>
        /// Validates the model <see cref="PayRequestModel"/>.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="errors">The errors.</param>
        public static void ValidateModel (this PayRequestModel model, List<PayError> errors) {
            if (string.IsNullOrWhiteSpace (model.Api))
                errors.Add (new PayError {
                    Code = "-1",
                        Description = "ارسال API الزامی‌ست!"
                });

            if (!double.TryParse (model.Amount.ToString (CultureInfo.InvariantCulture), out _))
                errors.Add (new PayError {
                    Code = "-3",
                        Description = "مبلغ پرداخت الزامیست!"
                });

            if (model.Amount < 1000)
                errors.Add (new PayError {
                    Code = "-4",
                        Description = "مبلغ پرداخت الزامیست!"
                });

            if (string.IsNullOrWhiteSpace (model.CallbackUrl))
                errors.Add (new PayError {
                    Code = "-5",
                        Description = "ارسال آدرس بازگشتی الزامی‌ست!"
                });
        }
    }
}