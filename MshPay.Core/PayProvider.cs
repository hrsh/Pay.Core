using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MshPay.Core.Config;
using MshPay.Core.Helper;
using MshPay.Core.Models;
using Newtonsoft.Json;

namespace MshPay.Core
{
    public class PayProvider : IPayProvider
    {
        private readonly PayConfiguration _configuration;

        private readonly HttpClient _httpClient;

        private readonly string _requestUrl;

        private readonly string _verifyUrl;

        public PayProvider(IOptionsSnapshot<PayConfiguration> options, HttpClient httpClient)
        {
            _httpClient = httpClient;
            if (_httpClient == null)
                throw new ArgumentNullException(nameof(_httpClient));

            var configuration = options;
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            _configuration = configuration.Value;
            if (_configuration == null)
                throw new ArgumentNullException(nameof(_configuration));

            _requestUrl = PayUrls.AuthorizeUrl;
            _verifyUrl = PayUrls.VerifyUrl;
        }

        private async Task<PayResult<T>> PostRequestBase<T, TU>(TU model, string url) where T : class
        {
            try
            {
                var t = await _httpClient.PostAsync(
                    url,
                    new StringContent(JsonConvert.SerializeObject(model),
                        Encoding.UTF8, "application/json"));

                if (t.StatusCode != HttpStatusCode.OK)
                {
                    return PayResult<T>.Failed(new PayError
                    {
                        Code = t.StatusCode,
                        Description =
                            $"Couldn't send request or request is not valid! Server respond with status code {t.StatusCode}."
                    });
                }

                var f = await t.Content.ReadAsAsync<T>();
                return PayResult<T>.Invoke(f);
            }
            catch (Exception exception)
            {
                return PayResult<T>.Failed(new PayError
                {
                    Code = "1000",
                    Description = $"Could not send request!\n{exception}."
                });
            }
        }

        public async Task<PayResult<PayResponseModel>> AuthorizeAsync(PayRequestModel model)
        {
            var errors = new List<PayError>();

            model.Api = _configuration.Api;

            model.ValidateModel(errors);
            if (errors.Any())
                return PayResult<PayResponseModel>.Failed(errors.ToArray());

            var t = await PostRequestBase<PayResponseModel, PayRequestModel>(model, _requestUrl);
            if (t.Succeeded) return PayResult<PayResponseModel>.Invoke(t.Result);

            errors.AddRange(t.Errors);
            return PayResult<PayResponseModel>.Failed(errors.ToArray());
        }

        public async Task<PayResult<VerifyResponseModel>> VerifyAsync(VerifyRequestModel model)
        {
            var errors = new List<PayError>();
            model.Api = _configuration.Api;
            model.ValidateVerifyRequestModel(errors);
            if (errors.Any()) return PayResult<VerifyResponseModel>.Failed(errors.ToArray());

            var t = await PostRequestBase<VerifyResponseModel, VerifyRequestModel>(model, _verifyUrl);
            return PayResult<VerifyResponseModel>.Invoke(t.Result);
        }
    }
}