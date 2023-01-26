using ArcaeaUnlimitedAPI.Lib.Models;
using Microsoft.EntityFrameworkCore;
using YukiChan.Shared.Models.Arcaea;
using YukiChan.Shared.Utils;
using ArcaeaDifficulty = YukiChan.Shared.Models.Arcaea.ArcaeaDifficulty;

namespace YukiChan.Server.Models.Arcaea.Factories;

public static class ArcaeaRecordFactory
{
    public static ArcaeaRecord FromAua(AuaRecord record, AuaChartInfo chartInfo)
    {
        return new ArcaeaRecord
        {
            Name = chartInfo.NameEn,
            SongId = record.SongId,
            Potential = record.Rating,
            Difficulty = (ArcaeaDifficulty)record.Difficulty,
            Rating = chartInfo.Rating / 10d,
            DisplayRating = chartInfo.Rating.GetRatingText(),
            Score = record.Score,
            ClearType = (ArcaeaClearType)record.ClearType!,
            Grade = ArcaeaSharedUtils.GetGrade(record.Score),
            //
            ShinyPureCount = record.ShinyPerfectCount,
            PureCount = record.PerfectCount,
            FarCount = record.NearCount,
            LostCount = record.MissCount,
            RecollectionRate = record.Health!.Value,
            //
            JacketOverride = chartInfo.JacketOverride,
            PlayTime = DateTimeOffset.FromUnixTimeMilliseconds(record.TimePlayed).UtcDateTime
        };
    }

    public static ArcaeaRecord FromAla(AlaRecord record, DbSet<ArcaeaSongDbChart> songDbCharts)
    {
        var chart = songDbCharts
            .FirstAsync(chart => chart.SongId == record.SongId && chart.RatingClass == record.Difficulty)
            .GetAwaiter().GetResult();

        return new ArcaeaRecord
        {
            Name = chart.NameEn,
            SongId = record.SongId,
            Potential = ArcaeaSharedUtils.CalculatePotential((double)chart.Rating / 10, record.Score),
            Difficulty = (ArcaeaDifficulty)record.Difficulty,
            Rating = chart.Rating / 10d,
            DisplayRating = chart.Rating.GetRatingText(),
            Score = record.Score,
            ClearType = record.RecollectionRate >= 70 ? ArcaeaClearType.NormalClear : ArcaeaClearType.TrackLost,
            Grade = ArcaeaSharedUtils.GetGrade(record.Score),
            //
            ShinyPureCount = record.ShinyPureCount,
            PureCount = record.PureCount,
            FarCount = record.FarCount,
            LostCount = record.LostCount,
            RecollectionRate = record.RecollectionRate,
            //
            JacketOverride = chart.JacketOverride,
            PlayTime = DateTimeOffset.FromUnixTimeMilliseconds(record.TimePlayed).UtcDateTime
        };
    }

    public static ArcaeaRecord GenerateFake(ArcaeaSongDbChart chart)
    {
        return new ArcaeaRecord
        {
            Name = chart.NameEn,
            SongId = chart.SongId,
            Potential = chart.Rating / 10d + 2,
            Rating = chart.Rating / 10d,
            DisplayRating = chart.Rating.GetRatingText(),
            Difficulty = (ArcaeaDifficulty)chart.RatingClass,
            Score = 10_000_000 + chart.Note,
            ShinyPureCount = chart.Note,
            PureCount = chart.Note,
            FarCount = 0,
            LostCount = 0,
            ClearType = ArcaeaClearType.PureMemory,
            Grade = ArcaeaGrade.EXP,
            RecollectionRate = 100,
            JacketOverride = chart.JacketOverride,
            PlayTime = DateTimeOffset.FromUnixTimeSeconds(chart.Date).UtcDateTime
        };
    }
}