using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace YukiChan.Server.Controllers;

[ApiController]
[Authorize]
[ApiVersion(1)]
[Route("x/v{version:apiVersion}/test")]
public sealed class TestController : YukiController
{
    [HttpGet]
    public IActionResult OnGetTest()
    {
        throw new NotImplementedException();
    }
}