using EnterpriseArchitecture.Business.Abstract;
using EnterpriseArchitecture.Core.Utilities.Hashing;
using EnterpriseArchitecture.Core.Utilities.Result.Abstract;
using EnterpriseArchitecture.Core.Utilities.Result.Concrete;
using EnterpriseArchitecture.DataAccess.Abstract;
using EnterpriseArchitecture.DataTransformationObjects.Concrete.Auth;
using EnterpriseArchitecture.DataTransformationObjects.Concrete.User;
using EnterpriseArchitecture.Entities.Concrete;

namespace EnterpriseArchitecture.Business.Concrete;

public class UserManager: IUserService
{
    private readonly IUserDal _userDal;
    public UserManager(IUserDal userDal)
    {
        _userDal = userDal;
    }

    public void Add(RegisterDto addUserDto)
    {
        FileInfo fileInfo = new FileInfo(addUserDto.ImageFile.FileName);
        string fileName = Guid.NewGuid().ToString();
        string fileFormat = fileInfo.Extension;
        fileName = $"{fileName}.{fileFormat}";
        string path = Path.Combine(Directory.GetCurrentDirectory(), "Content", "Images", fileName);
        using var stream = File.Create(path);
        addUserDto.ImageFile?.CopyTo(stream);
        
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
        
        _userDal.Add(user);
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