using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YukiChan.Server.Services.Console;
using YukiChan.Shared.Data.Console.Guilds;

namespace YukiChan.Server.Controllers.Console;

[ApiController]
[Authorize]
[ApiVersion(1)]
[Route("console/v{version:apiVersion}/guilds")]
public sealed class GuildsController : YukiController
{
    private readonly GuildsService _service;

    public GuildsController(GuildsService service)
    {
        _service = service;
    }

    [HttpGet("{platform}/{guildId}/assignee")]
    public async Task<IActionResult> OnGetAssignee(string platform, string guildId)
    {
        var assignee = await _service.GetAssignee(platform, guildId);
        return assignee is not null
            ? OkResp(new GuildAssigneeResponse { Assignee = assignee })
            : NotFoundResp();
    }

    [HttpPut("{platform}/{guildId}/assignee")]
    public async Task<IActionResult> OnUpdateAssignee(string platform, string guildId,
        [FromBody] GuildUpdateAssigneeRequest req)
    {
        await _service.UpdateAssignee(platform, guildId, req.NewAssigneeId);
        return OkResp();
    }
}