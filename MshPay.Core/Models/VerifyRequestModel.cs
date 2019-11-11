using System.Collections.Generic;
using MshPay.Core.Config;
using Newtonsoft.Json;

namespace MshPay.Core.Models
{
    public class VerifyRequestModel
    {
        [JsonProperty("api")] internal string Api { get; set; }

        [JsonProperty("token")] public string Token { get; set; }

        [JsonIgnore] public string Status { get; set; }

        public VerifyRequestModel(string token)
        {
            Token = token;
        }
    }

    internal static class VerifyRequestModelExtension
    {
        public static void ValidateVerifyRequestModel(this VerifyRequestModel model, List<PayError> errors)
        {
            if (string.IsNullOrWhiteSpace(model.Api))
                errors.Add(new PayError
                {
                    Code = "-1",
                    Description = "ارسال API الزامی‌ست!"
                });

            if (string.IsNullOrWhiteSpace(model.Token))
                errors.Add(new PayError
                {
                    Code = "-2",
                    Description = "ارسال Token الزامی‌ست!"
                });

            if (string.IsNullOrWhiteSpace(model.Status))
                errors.Add(new PayError
                {
                    Code = "-3",
                    Description = "وضعیت پرداخت مشخص نیست!"
                });

            if (model.Status == "0")
                errors.Add(new PayError
                {
                    Code = "-4",
                    Description = "تراکنش لغو شد!"
                });
        }
    }
}