using Microsoft.AspNetCore.Mvc;

namespace APICatalog.Utils;

public static class ErrorResultHelper
{
    public static ActionResult InternalServerErrorResult(ControllerBase controller)
    {
        return controller.StatusCode(StatusCodes.Status500InternalServerError,
            "An error occurred while processing your request. What a shame, isn't it?");
    }
}