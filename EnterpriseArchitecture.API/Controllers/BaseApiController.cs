using EnterpriseArchitecture.Core.Utilities.Result.Abstract;
using EnterpriseArchitecture.Core.Utilities.Result.Concrete;
using Microsoft.AspNetCore.Mvc;
using IResult = EnterpriseArchitecture.Core.Utilities.Result.Abstract.IResult;

namespace EnterpriseArchitecture.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class BaseApiController: ControllerBase
{
    [NonAction]
    protected async Task<IActionResult> HandleResult(IResult? result)
    {
        if (result == null) return await Task.FromResult<IActionResult>(BadRequest());

        if (!result.IsSuccess) return await Task.FromResult<IActionResult>(NotFound(result.Message));

        return await Task.FromResult<IActionResult>(Ok(result.Message));
    }

    [NonAction]
    protected async Task<IActionResult> HandleResult<T>(IDataResult<T>? result)
    {
        if (result == null) return await Task.FromResult<IActionResult>(BadRequest());

        if (!result.IsSuccess && result.Data == null) return NotFound(result.Message);
        
        return Ok(result);
    }
}