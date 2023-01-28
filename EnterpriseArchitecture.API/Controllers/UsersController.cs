using EnterpriseArchitecture.Business.Repositories.UserRepository;
using EnterpriseArchitecture.DataTransformationObjects.Concrete.Auth;
using EnterpriseArchitecture.DataTransformationObjects.Concrete.User;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseArchitecture.API.Controllers;

public class UsersController: BaseApiController
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = _userService.GetAllUsersWithId();
        return await HandleResult<List<UserForListWithOnlyIdDto>>(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var user = _userService.FindById(id);
        return await HandleResult<UserForListDto>(user);
    }

    [HttpPost]
    public async Task<IActionResult> Add(RegisterDto registerDto)
    {
        return await HandleResult(_userService.Add(registerDto));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UserForUpdateDto userForUpdateDto)
    {
        return await HandleResult(_userService.Update(userForUpdateDto));
    }

    [HttpPut("changePassword")]
    public async Task<IActionResult> Update(UserForChangePasswordDto userForChangePasswordDto)
    {
        return await HandleResult(_userService.ChangePassword(userForChangePasswordDto));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        return await HandleResult(_userService.RemoveById(id));
    }
}