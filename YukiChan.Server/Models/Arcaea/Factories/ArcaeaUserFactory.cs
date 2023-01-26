using ArcaeaUnlimitedAPI.Lib.Models;
using YukiChan.Shared.Models.Arcaea;

namespace YukiChan.Server.Models.Arcaea.Factories;

public static class ArcaeaUserFactory
{
    public static ArcaeaUser FromAua(AuaAccountInfo info)
    {
        return new ArcaeaUser
        {
            Name = info.Name,
            Code = info.Code,
            Potential = (double)info.Rating / 100,
            JoinTime = DateTimeOffset.FromUnixTimeMilliseconds(info.JoinDate).UtcDateTime,
            PartnerId = info.Character,
            IsPartnerAwakened = info.IsCharUncapped
        };
    }

    public static ArcaeaUser FromAla(AlaUser info, string usercode)
    {
        return new ArcaeaUser
        {
            Name = info.DisplayName,
            Code = usercode,
            Potential = info.Potential / 100,
            PartnerId = info.Partner.PartnerId,
            IsPartnerAwakened = info.Partner.IsAwakened
        };
    }
}