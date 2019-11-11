using System.Threading.Tasks;
using MshPay.Core.Helper;
using MshPay.Core.Models;

namespace MshPay.Core
{
    public interface IPayProvider
    {
        Task<PayResult<PayResponseModel>> AuthorizeAsync (PayRequestModel model);
        Task<PayResult<VerifyResponseModel>> VerifyAsync (VerifyRequestModel model);
    }
}