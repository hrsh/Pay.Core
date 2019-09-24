#### Pay.Core
A tiny library based on [pay.ir](https://www.pay.ir) RESTApi.

#### How to install
Install Nuget package:
`PM`

#### How to use
1. Add `PayConfig` section in your `appsettings.json`:
```json
//appsettings.json
"PayConfig": {
  "api": "your api access token goes here, or use test"
}
```
2. Add `PayService` in your `startup.cs` file:
```cs
//startup.cs
...
services.Configure<PayConfiguration>(option => Configuration.GetSection("PayConfig").Bind(option));
services.AddPay();
...
```
3. In your controller:
```cs
public class HomeController : Controller
{
    private readonly IPayProvider _payProvider;

    public HomeController(IPayProvider payProvider)
    {
        _payProvider = payProvider;
    }

    public async Task<IActionResult> Pay()
    {
        var t = await _payProvider.AuthorizeAsync(
            new PayRequestModel(
                1000,
                Url.Action("PayResult", "Home", null, Request.Scheme))
            {
                Description = "Description goes here...",
                InvoiceNumber = Guid.NewGuid().ToString("D"),// use your own...
                Mobile = "mobile number goes here"
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
}
```