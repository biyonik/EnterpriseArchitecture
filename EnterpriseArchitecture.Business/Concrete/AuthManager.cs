using EnterpriseArchitecture.Business.Abstract;
using EnterpriseArchitecture.Business.ValidationRules.FluentValidation;
using EnterpriseArchitecture.Core.CrossCuttingConcerns.Validation;
using EnterpriseArchitecture.Core.Utilities.Hashing;
using EnterpriseArchitecture.DataTransformationObjects.Concrete.Auth;
using EnterpriseArchitecture.DataTransformationObjects.Concrete.User;
using EnterpriseArchitecture.Entities.Concrete;
using FluentValidation.Results;

namespace EnterpriseArchitecture.Business.Concrete;

public class AuthManager: IAuthService
{
    private readonly IUserService _userService;

    public AuthManager(IUserService userService)
    {
        _userService = userService;
    }

    public string Register(RegisterDto registerDto)
    {
        ValidationTool.Validate(new UserValidator(), registerDto);
        _userService.Add(registerDto);
        return "";
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