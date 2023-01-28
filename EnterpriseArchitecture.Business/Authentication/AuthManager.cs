using EnterpriseArchitecture.Business.Authentication.Constants;
using EnterpriseArchitecture.Business.Authentication.Validation.FluentValidation;
using EnterpriseArchitecture.Business.Repositories.UserRepository;
using EnterpriseArchitecture.Core.Aspects.Validation;
using EnterpriseArchitecture.Core.Utilities.Business;
using EnterpriseArchitecture.Core.Utilities.Hashing;
using EnterpriseArchitecture.Core.Utilities.Result.Abstract;
using EnterpriseArchitecture.Core.Utilities.Result.Concrete;
using EnterpriseArchitecture.DataTransformationObjects.Concrete.Auth;
using EnterpriseArchitecture.Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace EnterpriseArchitecture.Business.Authentication;

public class AuthManager : IAuthService
{
    private readonly IUserService _userService;

    public AuthManager(IUserService userService)
    {
        _userService = userService;
    }

    [ValidationAspect(typeof(AuthValidator))]
    public IResult Register(RegisterDto registerDto)
    {
        
        IResult ruleResult = BusinessRule.Run(
            CheckIfEmailIsExist(registerDto.Email),
            CheckIfImageExtensionAllow(registerDto.ImageFile),
            CheckIfImageSizeIsLessThanOneMegabytes(registerDto.ImageFile, registerDto.ImageFile.Length)
        )!;

        if (ruleResult is { IsSuccess: false })
        {
            return new ErrorResult(ruleResult.Message);
        }
        
        IResult addResult = _userService.Add(registerDto);
        return addResult.IsSuccess 
            ? new SuccessResult(AuthMessages.RegisterSuccess)
            : new ErrorResult(AuthMessages.RegisterFail);
    }

    public IDataResult<User> Login(LoginDto loginDto)
    {
        IResult? ruleResult = BusinessRule.Run(
            CheckIfEmailIsExist(loginDto.Email)
        );
        if (ruleResult.IsSuccess) return new ErrorDataResult<User>(AuthMessages.UserNotFound);

        var userByEmail = _userService.GetByEmail(loginDto.Email).Data;

        var result =
            HashingHelper.VerifyPasswordHash(loginDto.Password, userByEmail?.PasswordHash, userByEmail?.PasswordSalt);
        if (!result) return new ErrorDataResult<User>(AuthMessages.UserInfoWrong);

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
        return isExist != null ? new ErrorResult(AuthMessages.EmailAlreadyUsed) : new SuccessResult();
    }

    private IResult CheckIfImageSizeIsLessThanOneMegabytes(IFormFile image, long imageSize)
    {
        var convertedSize = Convert.ToDecimal(imageSize * 0.000001);
        return convertedSize > 1
            ? new ErrorResult(AuthMessages.ImageSizeLimitError("1", "MB"))
            : new SuccessResult();
    }

    private IResult CheckIfImageExtensionAllow(IFormFile image)
    {
        if (image != null)
        {
            FileInfo fileInfo = new FileInfo(image.FileName);
            string? ext = fileInfo.Extension;
            string? extension = ext?.ToLower();
            List<string> allowedFileExtensions = new List<string> { ".jpg", ".jpeg", ".png" };
            return extension != null && !allowedFileExtensions.Contains(extension)
                ? new ErrorResult(AuthMessages.WrongFileFormat)
                : new SuccessResult();
        }

        return new ErrorResult(AuthMessages.FileNotReaded);
    }
}