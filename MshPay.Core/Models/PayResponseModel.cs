using MshPay.Core.Config;
using Newtonsoft.Json;

namespace MshPay.Core.Models
{
    public class PayResponseModel
    {
        [JsonProperty("status")] public string Status { get; set; }

        [JsonProperty("token")] public string Token { get; set; }

        public string PaymentUrl => !string.IsNullOrWhiteSpace(Token) && Status == "1"
            ? string.Format(PayUrls.PaymentUrl, Token)
            : "";
    }
}