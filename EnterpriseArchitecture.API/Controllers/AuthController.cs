using EnterpriseArchitecture.Business.Abstract;
using EnterpriseArchitecture.Core.Utilities.Result.Abstract;
using EnterpriseArchitecture.DataTransformationObjects.Concrete.Auth;
using EnterpriseArchitecture.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseArchitecture.API.Controllers;

public class AuthController: BaseApiController
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Register([FromForm]RegisterDto registerDto)
    {
        return await HandleResult(_authService.Register(registerDto));
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        return await HandleResult<User>(_authService.Login(loginDto));
    }


}