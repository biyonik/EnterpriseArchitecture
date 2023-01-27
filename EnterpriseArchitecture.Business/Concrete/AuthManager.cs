using EnterpriseArchitecture.Business.Abstract;
using EnterpriseArchitecture.Business.ValidationRules.FluentValidation;
using EnterpriseArchitecture.Core.Aspects.Validation;
using EnterpriseArchitecture.Core.Utilities.Business;
using EnterpriseArchitecture.Core.Utilities.Hashing;
using EnterpriseArchitecture.Core.Utilities.Result.Abstract;
using EnterpriseArchitecture.Core.Utilities.Result.Concrete;
using EnterpriseArchitecture.DataTransformationObjects.Concrete.Auth;
using EnterpriseArchitecture.Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace EnterpriseArchitecture.Business.Concrete;

public class AuthManager : IAuthService
{
    private readonly IUserService _userService;

    public AuthManager(IUserService userService)
    {
        _userService = userService;
    }

    [ValidationAspect(typeof(UserValidator))]
    public IResult Register(RegisterDto registerDto)
    {
        
        IResult ruleResult = BusinessRule.Run(
            CheckIfEmailIsExist(registerDto.Email),
            CheckIfImageExtensionAllow(registerDto.ImageFile),
            CheckIfImageSizeIsLessThanOneMegabytes(registerDto.ImageFile, registerDto.ImageFile.Length)
        )!;

        if (!ruleResult.IsSuccess)
        {
            return new ErrorResult(ruleResult.Message);
        }
        
        _userService.Add(registerDto);
        return new SuccessResult("Kullanıcı kaydı başarıyla tamamlandı.");
    }

    public IDataResult<User> Login(LoginDto loginDto)
    {
        IResult? ruleResult = BusinessRule.Run(
            CheckIfEmailIsExist(loginDto.Email)
        );
        if (ruleResult.IsSuccess) return new ErrorDataResult<User>("Böyle bir kullanıcı bulunamadı!");

        var userByEmail = _userService.GetByEmail(loginDto.Email).Data;

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

    private IResult CheckIfImageSizeIsLessThanOneMegabytes(IFormFile image, long imageSize)
    {
        var convertedSize = Convert.ToDecimal(imageSize * 0.00001);
        return convertedSize > 1
            ? new ErrorResult("Yüklediğiniz resim boyutu en fazla 1 MB olabilir.")
            : new SuccessResult();
    }

    private IResult CheckIfImageExtensionAllow(IFormFile image)
    {
        if (image != null)
        {
            string? fileName = image.Name;
            string? ext = fileName?.Substring(fileName.LastIndexOf(".", StringComparison.Ordinal));
            string? extension = ext?.ToLower();
            List<string> allowedFileExtensions = new List<string> { ".jpg", ".jpeg", ".png" };
            return extension != null && !allowedFileExtensions.Contains(extension)
                ? new ErrorResult("Eklediğiniz resim formatı, uyumlu bir format değildi!")
                : new SuccessResult();
        }

        return new ErrorResult("Dosya okunamadı!");
    }
}