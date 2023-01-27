using EnterpriseArchitecture.Core.Utilities.Result.Abstract;
using EnterpriseArchitecture.DataTransformationObjects.Concrete.Auth;
using EnterpriseArchitecture.DataTransformationObjects.Concrete.User;

namespace EnterpriseArchitecture.Business.Repositories.UserRepository;

public interface IUserService
{
    IResult Add(RegisterDto registerDto);
    IDataResult<UserWithAllFields?> GetByEmail(string email); 
    IDataResult<List<UserWithIdDto>> GetAllUsersWithId();
    IDataResult<List<UserDto>> GetAllUsers();
    IDataResult<UserDto> FindById(Guid id);
    IResult RemoveById(Guid id);
    IResult Update(UpdateUserDto updateUserDto);
}