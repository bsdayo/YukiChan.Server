using Microsoft.AspNetCore.Mvc;
using YukiChan.Shared.Data;

namespace YukiChan.Server.Controllers;

public abstract class YukiController : ControllerBase
{
    private static YukiResponse Resp(YukiErrorCode code, string? message = null)
    {
        return new YukiResponse
        {
            Code = code,
            Message = code == YukiErrorCode.Ok ? message : message ?? code.GetMessage()
        };
    }

    private static YukiResponse<TData> Resp<TData>(TData data,
        YukiErrorCode code, string? message = null) where TData : class
    {
        return new YukiResponse<TData>
        {
            Code = code,
            Message = code == YukiErrorCode.Ok ? message : message ?? code.GetMessage(),
            Data = data
        };
    }

    protected IActionResult OkResp<TData>(TData data) where TData : class
        => Ok(Resp(data, YukiErrorCode.Ok));

    protected IActionResult OkResp(
        YukiErrorCode code = YukiErrorCode.Ok, string? message = null) =>
        Ok(Resp(code, message));

    protected IActionResult BadRequestResp(
        YukiErrorCode code = YukiErrorCode.BadRequest, string? message = null) =>
        BadRequest(Resp(code, message));

    protected IActionResult BadRequestResp<TData>(TData data,
        YukiErrorCode code = YukiErrorCode.BadRequest, string? message = null) where TData : class =>
        BadRequest(Resp(data, code, message));

    protected IActionResult NotFoundResp(
        YukiErrorCode code = YukiErrorCode.NotFound, string? message = null) =>
        NotFound(Resp(code, message));
}