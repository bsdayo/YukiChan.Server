﻿using ArcaeaUnlimitedAPI.Lib.Utils;
using YukiChan.Shared.Data;

namespace YukiChan.Server.Utils;

public static class AuaExceptionExtensions
{
    public static YukiResponse ToYukiResponse(this AuaException auaEx)
    {
        return new YukiResponse
        {
            Code = YukiErrorCode.Arcaea_AuaErrorStart + auaEx.Status,
            Message = auaEx.Status switch
            {
                -1 => "用户名或好友码输入错误~",
                -2 => "好友码输入错误，请检查格式~",
                -3 => "用户未找到，请检查是否输入有误~",
                -4 => "查询到的用户过多。",
                -5 => "曲目名称或 ID 错误，请检查输入~",
                -6 => "曲目 ID 错误，请检查输入~",
                -7 => "曲目还没有记录在案呢...",
                -8 => "查询到的记录过多。",
                -9 => "输入的难度格式不正确。",
                -10 => "recent/overflow 参数错误。",
                -11 => "分配查分账号失败，请稍候再试。",
                -12 => "清除好友失败，请稍候再试~",
                -13 => "添加好友失败，请稍候再试~",
                -14 => "该曲目没有此难度哦~",
                -15 => "还没有有玩过该曲目哦~",
                -16 => "用户处于排行榜封禁中，无法使用 ArcaeaUnlimitedAPI 进行查分，请换用官方 API 重试。",
                -17 => "Best30 查询失败，请重试。",
                -18 => "更新服务暂不可用。",
                -19 => "请求的搭档无效。",
                -20 => "请求的文件不可用。",
                -21 => "区间格式输入错误。",
                -22 => "定数最大值低于最小值。",
                -23 => "用户潜力值低于 7.00，无法使用 ArcaeaUnlimitedAPI 进行查分。",
                -24 => "ArcaeaUnlimitedAPI 需要进行更新，请联系维护人员。",
                -233 => "API 内部发生错误。",
                _ => $"请求 API 时发生了错误。\n({auaEx.Status}: {auaEx.Message})"
            }
        };
    }
}