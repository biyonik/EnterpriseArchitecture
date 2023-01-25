using EnterpriseArchitecture.Business.Abstract;
using EnterpriseArchitecture.DataTransformationObjects.Concrete.Auth;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseArchitecture.API.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/v1/[controller]")]
public class AuthController: ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Register(RegisterDto registerDto)
    {
        var result = await Task.Run(() => _authService.Register(registerDto));
        return await Task.FromResult<IActionResult>(Ok(result));
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        var result = _authService.Login(loginDto);
        if (result == null) return await Task.FromResult<IActionResult>(NotFound());
        return await Task.FromResult<IActionResult>(Ok(result));
    }


}