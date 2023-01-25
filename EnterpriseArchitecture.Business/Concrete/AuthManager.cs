using EnterpriseArchitecture.Business.Abstract;
using EnterpriseArchitecture.Business.ValidationRules.FluentValidation;
using EnterpriseArchitecture.Core.CrossCuttingConcerns.Validation;
using EnterpriseArchitecture.Core.Utilities.Business;
using EnterpriseArchitecture.Core.Utilities.Hashing;
using EnterpriseArchitecture.Core.Utilities.Result.Abstract;
using EnterpriseArchitecture.Core.Utilities.Result.Concrete;
using EnterpriseArchitecture.DataTransformationObjects.Concrete.Auth;
using EnterpriseArchitecture.DataTransformationObjects.Concrete.User;
using EnterpriseArchitecture.Entities.Concrete;
using FluentValidation.Results;

namespace EnterpriseArchitecture.Business.Concrete;

public class AuthManager : IAuthService
{
    private readonly IUserService _userService;

    public AuthManager(IUserService userService)
    {
        _userService = userService;
    }

    public IResult Register(RegisterDto registerDto)
    {
        int imageSize = 2;
        ValidationTool.Validate(new UserValidator(), registerDto);

        IResult? result = BusinessRule.Run(
            CheckIfEmailIsExist(registerDto.Email),
            CheckIfImageSizeIsLessThanOneMegabytes(imageSize)
        );
        
        _userService.Add(registerDto);
        return new SuccessResult("Kullanıcı kaydı başarıyla tamamlandı.");
    }

    public IDataResult<User> Login(LoginDto loginDto)
    {
        IResult? ruleResult = BusinessRule.Run(
            CheckIfEmailIsExist(loginDto.Email)
        );

        var userByEmail = _userService.GetByEmail(loginDto.Email);

        var result =
            HashingHelper.VerifyPasswordHash(loginDto.Password, userByEmail?.PasswordHash, userByEmail?.PasswordSalt);
        if (!result) return new ErrorDataResult<User>("Kullanıcı bilgileriniz yanlış, lütfen tekrar deneyiniz.");

        var user = new User
        {
            Id = userByEmail.Id,
            Email = userByEmail.Email,
            Name = userByEmail.Name,
            ImageUrl = userByEmail.ImageUrl
        };
        return new SuccessDataResult<User>(user);
    }

    private IResult CheckIfEmailIsExist(string email)
    {
        var isExist = _userService.GetByEmail(email);
        return isExist != null ? new SuccessResult() : new ErrorResult("Bu mail adresi daha önce kullanılmış!");
    }

    private IResult CheckIfImageSizeIsLessThanOneMegabytes(int imageSize)
    {
        return imageSize > 1
            ? new ErrorResult("Yüklediğiniz resim boyutu en fazla 1 MB olabilir.")
            : new SuccessResult();
    }
}