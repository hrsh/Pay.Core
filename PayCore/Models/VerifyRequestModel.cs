// ***********************************************************************
// Assembly         : PayCore
// Author           : Mazdak Shojaie
// Created          : 09-24-2019
//
// Last Modified By : Mazdak Shojaie
// Last Modified On : 09-24-2019
// ***********************************************************************
// <copyright file="VerifyRequestModel.cs" company="PayCore">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using Newtonsoft.Json;
using PayCore.Config;

namespace PayCore.Models {
    /// <summary>
    /// Class VerifyRequestModel.
    /// </summary>
    public class VerifyRequestModel {
        /// <summary>
        /// Gets or sets the API.
        /// </summary>
        /// <value>The API.</value>
        [JsonProperty ("api")]
        internal string Api { get; set; }

        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        /// <value>The token.</value>
        [JsonProperty ("token")]
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        [JsonIgnore]
        public string Status { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="VerifyRequestModel"/> class.
        /// </summary>
        /// <param name="token">The token.</param>
        public VerifyRequestModel (string token) {
            Token = token;
        }
    }

    /// <summary>
    /// Class VerifyRequestModelExtension.
    /// </summary>
    internal static class VerifyRequestModelExtension {
        /// <summary>
        /// Validates the verify request model <see cref="PayCore.Models.VerifyRequestModel"/>.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="errors">The errors.</param>
        public static void ValidateVerifyRequestModel (this VerifyRequestModel model, List<PayError> errors) {
            if (string.IsNullOrWhiteSpace (model.Api))
                errors.Add (new PayError {
                    Code = "-1",
                        Description = "ارسال API الزامی‌ست!"
                });

            if (string.IsNullOrWhiteSpace (model.Token))
                errors.Add (new PayError {
                    Code = "-2",
                        Description = "ارسال Token الزامی‌ست!"
                });

            if (string.IsNullOrWhiteSpace (model.Status))
                errors.Add (new PayError {
                    Code = "-3",
                        Description = "وضعیت پرداخت مشخص نیست!"
                });

            if (model.Status == "0")
                errors.Add (new PayError {
                    Code = "-4",
                        Description = "تراکنش لغو شد!"
                });
        }
    }
}