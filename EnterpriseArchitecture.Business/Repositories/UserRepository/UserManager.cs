using EnterpriseArchitecture.Business.Authentication.Constants;
using EnterpriseArchitecture.Business.Repositories.UserRepository.Constants;
using EnterpriseArchitecture.Business.Repositories.UserRepository.Validation.FluentValidation;
using EnterpriseArchitecture.Business.Repositories.Utilities;
using EnterpriseArchitecture.Core.Aspects.Validation;
using EnterpriseArchitecture.Core.Utilities.Business;
using EnterpriseArchitecture.Core.Utilities.Hashing;
using EnterpriseArchitecture.Core.Utilities.Result.Abstract;
using EnterpriseArchitecture.Core.Utilities.Result.Concrete;
using EnterpriseArchitecture.DataAccess.Repositories.UserRepository;
using EnterpriseArchitecture.DataTransformationObjects.Concrete.Auth;
using EnterpriseArchitecture.DataTransformationObjects.Concrete.User;
using EnterpriseArchitecture.Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace EnterpriseArchitecture.Business.Repositories.UserRepository;

public class UserManager : IUserService
{
    private readonly IUserDal _userDal;
    private readonly IFileService _fileService;

    public UserManager(IUserDal userDal, IFileService fileService)
    {
        _userDal = userDal;
        _fileService = fileService;
    }

    public IResult Add(RegisterDto addUserDto)
    {
        string fileName = _fileService.Save(addUserDto.ImageFile, "Content", "Images");
        var user = CreateUser(addUserDto, fileName);

        var result = _userDal.Add(user);
        return result
            ? new SuccessResult(UserMessages.AddNewUserSuccess)
            : new ErrorResult(UserMessages.AddnewUserFailed);
    }

    private static User CreateUser(RegisterDto addUserDto, string fileName)
    {
        byte[] passwordHash, passwordSalt;
        HashingHelper.CreatePassword(addUserDto.Password, out passwordHash, out passwordSalt);
        User user = new User
        {
            Id = Guid.NewGuid(),
            Email = addUserDto.Email,
            Name = addUserDto.Name,
            ImageUrl = fileName,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
        };
        return user;
    }

    public IDataResult<UserForListWithAllFieldsDto?> GetByEmail(string email)
    {
        var stringComparer = StringComparer.OrdinalIgnoreCase;
        var isExists = _userDal.Get(u => stringComparer.Compare(u.Email, email) == 0);
        if (isExists == null) return null;

        var dto = new UserForListWithAllFieldsDto
        {
            Id = isExists.Id,
            Name = isExists.Name,
            Email = isExists.Email,
            ImageUrl = isExists.ImageUrl,
            PasswordHash = isExists.PasswordHash,
            PasswordSalt = isExists.PasswordSalt
        };
        return new SuccessDataResult<UserForListWithAllFieldsDto?>(dto);
    }

    public IDataResult<List<UserForListWithOnlyIdDto>> GetAllUsersWithId()
    {
        var result = new List<UserForListWithOnlyIdDto>();
        var users = _userDal.GetAll();
        foreach (var user in users)
        {
            var userDto = new UserForListWithOnlyIdDto
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                ImageUrl = user.ImageUrl
            };
            result.Add(userDto);
        }

        return result.Count > 0
            ? new SuccessDataResult<List<UserForListWithOnlyIdDto>>(result)
            : new ErrorDataResult<List<UserForListWithOnlyIdDto>>(UserMessages.UserNotFound);
    }

    public IDataResult<List<UserForListDto>> GetAllUsers()
    {
        var result = new List<UserForListDto>();
        var users = _userDal.GetAll();
        foreach (var user in users)
        {
            var userDto = new UserForListDto
            {
                Email = user.Email,
                Name = user.Name,
                ImageUrl = user.ImageUrl
            };
            result.Add(userDto);
        }

        return result.Count > 0
            ? new SuccessDataResult<List<UserForListDto>>(result)
            : new ErrorDataResult<List<UserForListDto>>(UserMessages.UserNotFound);
    }

    public IDataResult<UserForListDto> FindById(Guid id)
    {
        var user = _userDal.GetById(id);
        if (user == null) return new ErrorDataResult<UserForListDto>(UserMessages.UserNotFound);
        var userDto = new UserForListDto
        {
            Name = user.Name,
            Email = user.Email,
            ImageUrl = user.ImageUrl
        };
        return new SuccessDataResult<UserForListDto>(userDto);
    }

    public IDataResult<UserForListWithAllFieldsDto> FindByIdWithAllFields(Guid id)
    {
        var user = _userDal.GetById(id);
        if (user == null) return new ErrorDataResult<UserForListWithAllFieldsDto>(UserMessages.UserNotFound);
        var userDto = new UserForListWithAllFieldsDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            ImageUrl = user.ImageUrl,
            PasswordHash = user.PasswordHash,
            PasswordSalt = user.PasswordSalt
        };
        return new SuccessDataResult<UserForListWithAllFieldsDto>(userDto);
    }

    public IResult RemoveById(Guid id)
    {
        var user = _userDal.GetById(id);
        if (user == null) return new ErrorResult(UserMessages.UserNotFound);
        if (!string.IsNullOrEmpty(user.ImageUrl))
        {
            _fileService.Delete(user.ImageUrl, "Content", "Images");
        }
        var result = _userDal.Delete(user!);
        return result
            ? new SuccessResult(UserMessages.DeleteUserSuccess)
            : new ErrorResult(UserMessages.DeleteUserFailed);
    }

    [ValidationAspect(typeof(UserValidator))]
    public IResult Update(UserForUpdateDto userForUpdateDto)
    {
        var user = _userDal.GetById(userForUpdateDto.Id);
        if (user == null) return new ErrorResult(UserMessages.UserNotFound);
        
        var newUser = new User
        {
            Id = userForUpdateDto.Id,
            Email = userForUpdateDto.Email,
            Name = userForUpdateDto.Name,
            PasswordHash = user.PasswordHash,
            PasswordSalt = user.PasswordSalt,
            UserOperationClaim = user.UserOperationClaim
        };
        if (userForUpdateDto.Image != null)
        {
            IResult ruleResult = BusinessRule.Run(
                CheckIfImageExtensionAllow(userForUpdateDto.Image),
                CheckIfImageSizeIsLessThanOneMegabytes(userForUpdateDto.Image, userForUpdateDto.Image.Length)
            )!;

            if (ruleResult is { IsSuccess: false })
            {
                return new ErrorResult(ruleResult.Message);
            }

            if (!string.IsNullOrEmpty(user.ImageUrl)) _fileService.Delete(user.ImageUrl, "Content", "Images");
            
            string fileName = _fileService.Save(userForUpdateDto.Image, "Content", "Images");
            newUser.ImageUrl = fileName;
        }
        
        var result = _userDal.Update(newUser);
        return result
            ? new SuccessResult(UserMessages.UpdateUserSuccess)
            : new ErrorResult(UserMessages.UpdateUserFailed);
    }

    [ValidationAspect(typeof(ChangePasswordValidator))]
    public IResult ChangePassword(UserForChangePasswordDto userForChangePasswordDto)
    {
        User? user = _userDal.Get(x => x.Id == userForChangePasswordDto.Id);
        if (user == null) return new ErrorResult(UserMessages.UserNotFound);

        bool result = HashingHelper.VerifyPasswordHash(userForChangePasswordDto.CurrentPassword, user.PasswordHash,
            user.PasswordSalt);
        if (!result) return new ErrorResult(UserMessages.WrongCurrentPassword);

        byte[] passwordHash, passwordSalt;
        HashingHelper.CreatePassword(userForChangePasswordDto.NewPassword, out passwordHash, out passwordSalt);

        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;
        var updateResult = _userDal.Update(user);

        return updateResult
            ? new SuccessResult(UserMessages.PasswordChangeSuccess)
            : new ErrorResult(UserMessages.PasswordChangeFail);
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