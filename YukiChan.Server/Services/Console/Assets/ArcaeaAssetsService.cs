using ArcaeaUnlimitedAPI.Lib.Models;
using YukiChan.Server.Services.Console.Arcaea;
using YukiChan.Server.Utils;

namespace YukiChan.Server.Services.Console.Assets;

public sealed class ArcaeaAssetsService
{
    private readonly ArcaeaAuaService _auaService;

    public ArcaeaAssetsService(ArcaeaAuaService auaService)
    {
        _auaService = auaService;
    }

    public async Task<byte[]> GetSongCover(string songId, ArcaeaDifficulty difficulty)
    {
        var path = $"{YukiServerDir.ArcaeaCache}/song/{songId}-{(int)difficulty}.jpg";
        if (File.Exists(path))
            return await File.ReadAllBytesAsync(path);

        var songCover = await _auaService.AuaClient.Assets.Song(songId, AuaSongQueryType.SongId, difficulty);
        await File.WriteAllBytesAsync(path, songCover);
        return songCover;
    }

    public async Task<byte[]> GetCharImage(int charId, bool awakened)
    {
        var path = $"{YukiServerDir.ArcaeaCache}/char/{charId}{(awakened ? "-awakened.jpg" : ".jpg")}";
        if (File.Exists(path))
            return await File.ReadAllBytesAsync(path);

        var charImage = await _auaService.AuaClient.Assets.Char(charId, awakened);
        await File.WriteAllBytesAsync(path, charImage);
        return charImage;
    }

    public async Task<byte[]> GetPreviewImage(string songId, ArcaeaDifficulty difficulty)
    {
        var path = $"{YukiServerDir.ArcaeaCache}/preview/{songId}-{(int)difficulty}.jpg";
        if (File.Exists(path))
            return await File.ReadAllBytesAsync(path);

        var charImage = await _auaService.AuaClient.Assets.Preview(songId, difficulty);
        await File.WriteAllBytesAsync(path, charImage);
        return charImage;
    }

    public Task<byte[]> GetArcSongDb()
    {
        var path = $"{YukiServerDir.ArcaeaAssets}/arcsong.db";
        return File.ReadAllBytesAsync(path);
    }
}