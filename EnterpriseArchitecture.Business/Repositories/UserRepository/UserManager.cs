using EnterpriseArchitecture.Business.Repositories.Utilities;
using EnterpriseArchitecture.Core.Utilities.Hashing;
using EnterpriseArchitecture.Core.Utilities.Result.Abstract;
using EnterpriseArchitecture.Core.Utilities.Result.Concrete;
using EnterpriseArchitecture.DataAccess.Repositories.UserRepository;
using EnterpriseArchitecture.DataTransformationObjects.Concrete.Auth;
using EnterpriseArchitecture.DataTransformationObjects.Concrete.User;
using EnterpriseArchitecture.Entities.Concrete;

namespace EnterpriseArchitecture.Business.Repositories.UserRepository;

public class UserManager: IUserService
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
            ? new SuccessResult("Kullanıcı kayıt işlemi başarılı.")
            : new ErrorResult("Kullanıcı kayıt işlemi başarısız!");
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

    public IDataResult<UserWithAllFields?> GetByEmail(string email)
    {
        var stringComparer = StringComparer.OrdinalIgnoreCase;
        var isExists = _userDal.Get(u => stringComparer.Compare(u.Email, email) == 0);
        if (isExists == null) return null;
        
        var dto = new UserWithAllFields
        {
            Id = isExists.Id,
            Name = isExists.Name,
            Email = isExists.Email,
            ImageUrl = isExists.ImageUrl,
            PasswordHash = isExists.PasswordHash,
            PasswordSalt = isExists.PasswordSalt
        };
        return new SuccessDataResult<UserWithAllFields?>(dto);
    }

    public IDataResult<List<UserWithIdDto>> GetAllUsersWithId()
    {
        var result = new List<UserWithIdDto>();
        var users = _userDal.GetAll();
        foreach (var user in users)
        {
            var userDto = new UserWithIdDto
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                ImageUrl = user.ImageUrl
            };
            result.Add(userDto);
        }

        return result.Count > 0
            ? new SuccessDataResult<List<UserWithIdDto>>(result)
            : new ErrorDataResult<List<UserWithIdDto>>(
                "Sistemde kayıtlı herhangi bir kullanıcı olmadığı için kayıt getirilemedi");
    }

    public IDataResult<List<UserDto>> GetAllUsers()
    {
        var result = new List<UserDto>();
        var users = _userDal.GetAll();
        foreach (var user in users)
        {
            var userDto = new UserDto
            {
                Email = user.Email,
                Name = user.Name,
                ImageUrl = user.ImageUrl
            };
            result.Add(userDto);
        }

        return result.Count > 0
            ? new SuccessDataResult<List<UserDto>>(result)
            : new ErrorDataResult<List<UserDto>>(
                "Sistemde kayıtlı herhangi bir kullanıcı olmadığı için kayıt getirilemedi");
    }
    
    public IDataResult<UserDto> FindById(Guid id)
    {
        var user = _userDal.GetById(id);
        if (user == null) throw new Exception("User bulunamadı!");
        var userDto = new UserDto
        {
            Name = user.Name,
            Email = user.Email,
            ImageUrl = user.ImageUrl
        };
        return new SuccessDataResult<UserDto>(userDto);
    }

    public IResult RemoveById(Guid id)
    {
        var user = _userDal.GetById(id);
        var result = _userDal.Delete(user!);
        return result
            ? new SuccessResult("Kullanıcı silme işlemi başarılı.")
            : new ErrorResult("Kullanıcı silme işlemi başarısız!");
    }

    public IResult Update(UpdateUserDto updateUserDto)
    {
        var user = new User
        {
            Email = updateUserDto.Email,
            Name = updateUserDto.Name,
            ImageUrl = updateUserDto.ImageUrl
        };
        var result = _userDal.Update(user);
        return result
            ? new SuccessResult("Kullanıcı bilgileri güncelleme işlemi başarılı.")
            : new ErrorResult("Kullanıcı bilgilerini güncelleme işlemi başarısız!");
    }
}