using Newtonsoft.Json;

namespace MshPay.Core.Models
{
    public class VerifyResponseModel
    {
        [JsonProperty("status")] public string Status { get; set; }

        [JsonProperty("amount")] public double Amount { get; set; }

        [JsonProperty("transId")] public string TransactionId { get; set; }

        [JsonProperty("factorNumber")] public string InvoiceNumber { get; set; }

        [JsonProperty("mobile")] public string Mobile { get; set; }

        [JsonProperty("description")] public string Description { get; set; }

        [JsonProperty("cardNumber")] public string CardNumber { get; set; }

        [JsonProperty("message")] public string Message { get; set; }
    }
}