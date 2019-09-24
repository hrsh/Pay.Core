// ***********************************************************************
// Assembly         : PayCore
// Author           : Mazdak Shojaie
// Created          : 09-24-2019
//
// Last Modified By : Mazdak Shojaie
// Last Modified On : 09-24-2019
// ***********************************************************************
// <copyright file="PayProvider.cs" company="PayCore">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PayCore.Config;
using PayCore.Helper;
using PayCore.Models;

namespace PayCore {
    /// <summary>
    /// Class PayProvider.
    /// Implements the <see cref="PayCore.IPayProvider" />
    /// </summary>
    /// <seealso cref="PayCore.IPayProvider" />
    public class PayProvider : IPayProvider {
        /// <summary>
        /// The configuration
        /// </summary>
        private readonly PayConfiguration _configuration;
        /// <summary>
        /// The HTTP client
        /// </summary>
        private readonly HttpClient _httpClient;
        /// <summary>
        /// The request URL
        /// </summary>
        private readonly string _requestUrl;
        /// <summary>
        /// The verify URL
        /// </summary>
        private readonly string _verifyUrl;

        /// <summary>
        /// Initializes a new instance of the <see cref="PayProvider"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="httpClient">The HTTP client.</param>
        /// <exception cref="System.ArgumentNullException">
        /// _httpClient
        /// or
        /// configuration
        /// or
        /// _configuration
        /// </exception>
        public PayProvider (IOptionsSnapshot<PayConfiguration> options, HttpClient httpClient) {
            _httpClient = httpClient;
            if (_httpClient == null)
                throw new ArgumentNullException (nameof (_httpClient));

            var configuration = options;
            if (configuration == null)
                throw new ArgumentNullException (nameof (configuration));

            _configuration = configuration.Value;
            if (_configuration == null)
                throw new ArgumentNullException (nameof (_configuration));

            _requestUrl = PayUrls.AuthorizeUrl;
            _verifyUrl = PayUrls.VerifyUrl;
        }

        /// <summary>
        /// Posts the request.
        /// </summary>
        /// <typeparam name="T">Return type.</typeparam>
        /// <typeparam name="TU">Input model.</typeparam>
        /// <param name="model">The model.</param>
        /// <param name="url">The URL.</param>
        /// <returns>Task&lt;PayResult&lt;T&gt;&gt;.</returns>
        private async Task<PayResult<T>> PostRequestBase<T, TU> (TU model, string url) where T : class {
            try {
                var t = await _httpClient.PostAsync (
                    url,
                    new StringContent (JsonConvert.SerializeObject (model),
                        Encoding.UTF8, "application/json"));

                if (t.StatusCode != HttpStatusCode.OK) {
                    return PayResult<T>.Failed (new PayError {
                        Code = t.StatusCode,
                            Description = $"Couldn't send request or request is not valid! Server respond with status code {t.StatusCode}."
                    });
                }

                var f = await t.Content.ReadAsAsync<T> ();
                return PayResult<T>.Invoke (f);
            } catch (Exception exception) {
                return PayResult<T>.Failed (new PayError {
                    Code = "1000",
                        Description = $"Could not send request!\n{exception}."
                });
            }
        }

        /// <summary>
        /// authorize the request as an asynchronous operation.
        /// </summary>
        /// <param name="model">The payment model <see cref="PayCore.Models.PayRequestModel" />.</param>
        /// <returns>Task&lt;PayResult&lt;PayResponseModel&gt;&gt;.</returns>
        public async Task<PayResult<PayResponseModel>> AuthorizeAsync (PayRequestModel model) {
            var errors = new List<PayError> ();

            model.Api = _configuration.Api;

            model.ValidateModel (errors);
            if (errors.Any ())
                return PayResult<PayResponseModel>.Failed (errors.ToArray ());

            var t = await PostRequestBase<PayResponseModel, PayRequestModel> (model, _requestUrl);
            if (t.Succeeded) return PayResult<PayResponseModel>.Invoke (t.Result);

            errors.AddRange (t.Errors);
            return PayResult<PayResponseModel>.Failed (errors.ToArray ());

        }

        /// <summary>
        /// verifies the result as an asynchronous operation.
        /// </summary>
        /// <param name="model">The model <see cref="PayCore.Models.VerifyRequestModel" />.</param>
        /// <returns>Task&lt;PayResult&lt;VerifyResponseModel&gt;&gt;.</returns>
        public async Task<PayResult<VerifyResponseModel>> VerifyAsync (VerifyRequestModel model) {
            var errors = new List<PayError> ();
            model.Api = _configuration.Api;
            model.ValidateVerifyRequestModel (errors);
            if (errors.Any ()) return PayResult<VerifyResponseModel>.Failed (errors.ToArray ());

            var t = await PostRequestBase<VerifyResponseModel, VerifyRequestModel> (model, _verifyUrl);
            return PayResult<VerifyResponseModel>.Invoke (t.Result);
        }
    }
}