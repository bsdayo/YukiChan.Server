using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YukiChan.Server.Services.Console;
using YukiChan.Shared.Data.Console;

namespace YukiChan.Server.Controllers.Console;

[ApiController]
[Authorize]
[ApiVersion(1)]
[Route("console/v{version:apiVersion}")]
public sealed class RootController : YukiController
{
    private readonly PrecheckService _service;

    public RootController(PrecheckService service)
    {
        _service = service;
    }

    [HttpPost("precheck")]
    public async Task<IActionResult> OnPrecheck([FromBody] PrecheckRequest req)
    {
        var isAssignee = req.GuildId is null ||
                         await _service.CheckAssignee(req.Platform, req.GuildId, req.SelfId);
        var authority = await _service.GetUserAuthority(req.Platform, req.UserId);
        if (isAssignee)
            await _service.SaveCommandHistory(req, authority);
        return OkResp(new PrecheckResponse
        {
            IsAssignee = isAssignee,
            UserAuthority = authority
        });
    }
}