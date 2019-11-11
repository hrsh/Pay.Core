namespace MshPay.Core.Config
{
    public static class PayUrls
    {
        public const string AuthorizeUrl = "https://pay.ir/pg/send";
        public const string PaymentUrl = "https://pay.ir/pg/{0}";
        public const string VerifyUrl = "https://pay.ir/pg/verify";
    }
}