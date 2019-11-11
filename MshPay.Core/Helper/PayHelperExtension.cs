using Microsoft.Extensions.DependencyInjection;

namespace MshPay.Core.Helper
{
    public static class PayHelperExtension
    {
        public static IServiceCollection AddPay(this IServiceCollection service)
        {
            service.AddHttpClient<IPayProvider, PayProvider>();
            return service;
        }
    }
}