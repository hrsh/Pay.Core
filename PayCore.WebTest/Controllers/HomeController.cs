using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PayCore.Models;
using PayCore.WebTest.Models;

namespace PayCore.WebTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPayProvider _payProvider;

        public HomeController(IPayProvider payProvider)
        {
            _payProvider = payProvider;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Privacy()
        {
            var t = await _payProvider.AuthorizeAsync(
                new PayRequestModel(
                    1000,
                    Url.Action("PayResult", "Home", null, Request.Scheme))
                {
                    Description = "توضیحات تراکنش",
                    InvoiceNumber = Guid.NewGuid().ToString("D"),
                    Mobile = "09123456789"
                });

            if (t.Succeeded)
            {
                return Redirect(t.Result.PaymentUrl);
            }

            return Content(JsonConvert.SerializeObject(t.Errors, Formatting.Indented));
        }

        public async Task<IActionResult> PayResult(string status, string token)
        {
            //todo: validate status and token here ...
            var t = await _payProvider.VerifyAsync(new VerifyRequestModel(token) { Status = status });
            if (t.Succeeded)
                return Content(JsonConvert.SerializeObject(t.Result, Formatting.Indented));
            return Content(JsonConvert.SerializeObject(t.Errors, Formatting.Indented));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
