using EnterpriseArchitecture.Business.Abstract;
using EnterpriseArchitecture.Core.Utilities.Hashing;
using EnterpriseArchitecture.DataTransformationObjects.Concrete.Auth;
using EnterpriseArchitecture.Entities.Concrete;

namespace EnterpriseArchitecture.Business.Concrete;

public class AuthManager: IAuthService
{
    private readonly IUserService _userService;

    public AuthManager(IUserService userService)
    {
        _userService = userService;
    }

    public bool Register(RegisterDto registerDto)
    {
        return true;
    }

    public User? Login(LoginDto loginDto)
    {
        var userByEmail = _userService.GetByEmail(loginDto.Email);
        var result = HashingHelper.VerifyPasswordHash(loginDto.Password, userByEmail.PasswordHash, userByEmail.PasswordSalt);
        if (!result) return null;
        
        var user = new User
        {
            Id = userByEmail.Id,
            Email = userByEmail.Email,
            Name = userByEmail.Name,
            ImageUrl = userByEmail.ImageUrl
        };
        return user;

    }
}