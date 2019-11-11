using System.Collections.Generic;
using System.Globalization;
using MshPay.Core.Config;
using Newtonsoft.Json;

namespace MshPay.Core.Models
{
    public class PayRequestModel
    {
        [JsonProperty("api")] internal string Api { get; set; }

        [JsonProperty("amount")] public double Amount { get; set; }

        [JsonProperty("redirect")] public string CallbackUrl { get; set; }

        [JsonProperty("mobile")] public string Mobile { get; set; }

        [JsonProperty("factorNumber")] public string InvoiceNumber { get; set; }

        [JsonProperty("description")] public string Description { get; set; }

        [JsonProperty("validCardNumber")] public string ValidCardNumber { get; set; }

        public PayRequestModel(double amount, string callbackUrl)
        {
            Amount = amount;
            CallbackUrl = callbackUrl;
        }
    }

    internal static class PayRequestModelExtension
    {
        public static void ValidateModel(this PayRequestModel model, List<PayError> errors)
        {
            if (string.IsNullOrWhiteSpace(model.Api))
                errors.Add(new PayError
                {
                    Code = "-1",
                    Description = "ارسال API الزامی‌ست!"
                });

            if (!double.TryParse(model.Amount.ToString(CultureInfo.InvariantCulture), out _))
                errors.Add(new PayError
                {
                    Code = "-3",
                    Description = "مبلغ پرداخت الزامیست!"
                });

            if (model.Amount < 1000)
                errors.Add(new PayError
                {
                    Code = "-4",
                    Description = "مبلغ پرداخت الزامیست!"
                });

            if (string.IsNullOrWhiteSpace(model.CallbackUrl))
                errors.Add(new PayError
                {
                    Code = "-5",
                    Description = "ارسال آدرس بازگشتی الزامی‌ست!"
                });
        }
    }
}