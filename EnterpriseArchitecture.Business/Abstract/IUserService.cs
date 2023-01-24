using EnterpriseArchitecture.DataTransformationObjects.Concrete.User;

namespace EnterpriseArchitecture.Business.Abstract;

public interface IUserService
{
    void Add(AddUserDto addUserDto);
    UserWithAllFields? GetByEmail(string email); 
    List<UserWithIdDto> GetAllUsersWithId();
    List<UserDto> GetAllUsers();
    UserDto FindById(Guid id);
    void RemoveById(Guid id);
    void Update(UpdateUserDto updateUserDto);
}