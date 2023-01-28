using EnterpriseArchitecture.Business.Repositories.UserRepository.Constants;
using EnterpriseArchitecture.Business.Repositories.UserRepository.Validation.FluentValidation;
using EnterpriseArchitecture.Business.Repositories.Utilities;
using EnterpriseArchitecture.Core.Aspects.Validation;
using EnterpriseArchitecture.Core.Utilities.Hashing;
using EnterpriseArchitecture.Core.Utilities.Result.Abstract;
using EnterpriseArchitecture.Core.Utilities.Result.Concrete;
using EnterpriseArchitecture.DataAccess.Repositories.UserRepository;
using EnterpriseArchitecture.DataTransformationObjects.Concrete.Auth;
using EnterpriseArchitecture.DataTransformationObjects.Concrete.User;
using EnterpriseArchitecture.Entities.Concrete;

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
        if (user == null) throw new Exception(UserMessages.UserNotFound);
        var userDto = new UserForListDto
        {
            Name = user.Name,
            Email = user.Email,
            ImageUrl = user.ImageUrl
        };
        return new SuccessDataResult<UserForListDto>(userDto);
    }

    public IResult RemoveById(Guid id)
    {
        var user = _userDal.GetById(id);
        var result = _userDal.Delete(user!);
        return result
            ? new SuccessResult(UserMessages.DeleteUserSuccess)
            : new ErrorResult(UserMessages.DeleteUserFailed);
    }

    [ValidationAspect(typeof(UserValidator))]
    public IResult Update(UserForUpdateDto userForUpdateDto)
    {
        var user = new User
        {
            Email = userForUpdateDto.Email,
            Name = userForUpdateDto.Name,
            ImageUrl = userForUpdateDto.ImageUrl
        };
        var result = _userDal.Update(user);
        return result
            ? new SuccessResult(UserMessages.UpdateUserSuccess)
            : new ErrorResult(UserMessages.UpdateUserFailed);
    }

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
}