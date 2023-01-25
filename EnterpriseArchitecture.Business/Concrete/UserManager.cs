using EnterpriseArchitecture.Business.Abstract;
using EnterpriseArchitecture.DataAccess.Abstract;
using EnterpriseArchitecture.DataTransformationObjects.Concrete.Auth;
using EnterpriseArchitecture.DataTransformationObjects.Concrete.User;

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
        throw new NotImplementedException();
    }

    public UserWithAllFields? GetByEmail(string email)
    {
        var stringComparer = StringComparer.OrdinalIgnoreCase;
        var isExists = _userDal.Get(u => stringComparer.Compare(u.Email, email) == 0);
        if (isExists == null) throw new Exception("User bulunamadı!");
        
        var dto = new UserWithAllFields
        {
            Id = isExists.Id,
            Name = isExists.Name,
            Email = isExists.Email,
            ImageUrl = isExists.ImageUrl,
            PasswordHash = isExists.PasswordHash,
            PasswordSalt = isExists.PasswordSalt
        };
        return dto;
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