using System.Net.Mime;
using ArcaeaUnlimitedAPI.Lib.Models;
using ArcaeaUnlimitedAPI.Lib.Utils;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YukiChan.Server.Services.Console.Assets;
using YukiChan.Server.Utils;

namespace YukiChan.Server.Controllers.Console.Assets;

[ApiController]
[ApiVersion(1)]
[Authorize]
[Route("console/v{version:apiVersion}/assets/arcaea")]
public sealed class ArcaeaAssetsController : YukiController
{
    private readonly ArcaeaAssetsService _service;

    public ArcaeaAssetsController(ArcaeaAssetsService service)
    {
        _service = service;
    }

    [HttpGet("song/{songId}/{difficulty:int}")]
    public async Task<IActionResult> OnGetSongCover(string songId, int difficulty)
    {
        try
        {
            var cover = await _service.GetSongCover(songId, (ArcaeaDifficulty)difficulty);
            return File(cover, MediaTypeNames.Image.Jpeg);
        }
        catch (AuaException auaEx)
        {
            return NotFound(auaEx.ToYukiResponse());
        }
    }

    [HttpGet("char/{charId:int}")]
    public async Task<IActionResult> OnGetCharImage(int charId, [FromQuery] bool awakened)
    {
        try
        {
            var cover = await _service.GetCharImage(charId, awakened);
            return File(cover, MediaTypeNames.Image.Jpeg);
        }
        catch (AuaException auaEx)
        {
            return NotFound(auaEx.ToYukiResponse());
        }
    }

    [HttpGet("preview/{songId}/{difficulty:int}")]
    public async Task<IActionResult> OnGetPreviewImage(string songId, int difficulty)
    {
        try
        {
            var cover = await _service.GetPreviewImage(songId, (ArcaeaDifficulty)difficulty);
            return File(cover, MediaTypeNames.Image.Jpeg);
        }
        catch (AuaException auaEx)
        {
            return NotFound(auaEx.ToYukiResponse());
        }
    }
}