using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MshPay.Core;
using MshPay.Core.Models;
using Newtonsoft.Json;

namespace MshPay.WebAppTest.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IPayProvider _payProvider;

        public IndexModel(ILogger<IndexModel> logger, IPayProvider payProvider)
        {
            _logger = logger;
            _payProvider = payProvider;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostPayAsync()
        {
            var callbackUrl = Url.Page("./Index", "Verify", null, Request.Scheme);
            var t = await _payProvider.AuthorizeAsync(
                new PayRequestModel(
                    1000, callbackUrl)
                {
                    Description = "Description goes here...",
                    InvoiceNumber = Guid.NewGuid().ToString("D"), // use your own...
                    Mobile = "mobile number goes here"
                });

            if (t.Succeeded)
            {
                return Redirect(t.Result.PaymentUrl);
            }

            return Content(JsonConvert.SerializeObject(t.Errors, Formatting.Indented));
        }

        public async Task<IActionResult> OnGetVerifyAsync(string status, string token)
        {
            //todo: validate status and token here ...
            var t = await _payProvider.VerifyAsync(new VerifyRequestModel(token) {Status = status});
            return Content(t.Succeeded ? 
                JsonConvert.SerializeObject(t.Result, Formatting.Indented) : 
                JsonConvert.SerializeObject(t.Errors, Formatting.Indented));
        }
    }
}