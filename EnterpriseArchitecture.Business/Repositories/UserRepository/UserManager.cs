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

    public void Add(RegisterDto addUserDto)
    {
        string fileName = _fileService.Save(addUserDto.ImageFile, "Content", "Images");
        var user = CreateUser(addUserDto, fileName);

        _userDal.Add(user);
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

    public List<UserWithIdDto> GetAllUsersWithId()
    {
        throw new NotImplementedException();
    }

    public List<UserDto> GetAllUsers()
    {
        throw new NotImplementedException();
    }
    
    public UserDto FindById(Guid id)
    {
        var user = _userDal.GetById(id);
        if (user == null) throw new Exception("User bulunamadı!");
        var userDto = new UserDto
        {
            Name = user.Name,
            Email = user.Email,
            ImageUrl = user.ImageUrl
        };
        return userDto;
    }

    public void RemoveById(Guid id)
    {
        throw new NotImplementedException();
    }

    public void Update(UpdateUserDto updateUserDto)
    {
        throw new NotImplementedException();
    }
}