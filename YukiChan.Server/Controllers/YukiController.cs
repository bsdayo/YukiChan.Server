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

    private static YukiResponse<TData> Resp<TData>(TData data) where TData : class
    {
        return new YukiResponse<TData>
        {
            Code = YukiErrorCode.Ok,
            Data = data
        };
    }

    protected IActionResult OkResp<TData>(TData data) where TData : class => Ok(Resp(data));

    protected IActionResult OkResp(
        YukiErrorCode code = YukiErrorCode.Ok, string? message = null) =>
        Ok(Resp(code, message));

    protected IActionResult BadRequestResp(
        YukiErrorCode code = YukiErrorCode.BadRequest, string? message = null) =>
        BadRequest(Resp(code, message));

    protected IActionResult NotFoundResp(
        YukiErrorCode code = YukiErrorCode.NotFound, string? message = null) =>
        NotFound(Resp(code, message));
}