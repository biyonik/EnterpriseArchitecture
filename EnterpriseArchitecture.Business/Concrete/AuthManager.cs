using EnterpriseArchitecture.Business.Abstract;
using EnterpriseArchitecture.Business.ValidationRules.FluentValidation;
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

    public bool Register(RegisterDto registerDto)
    {
        UserValidator userValidator = new UserValidator();
        ValidationResult validationResult = userValidator.Validate(registerDto);
        if (validationResult.IsValid)
        {
            AddUserDto addUserDto = new()
            {
                Email = registerDto.Email,
                Name = registerDto.Name,
                Password = registerDto.Password,
                ImageUrl = registerDto.ImageUrl
            };
            _userService.Add(addUserDto);
            return true;
        }
        return false;
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