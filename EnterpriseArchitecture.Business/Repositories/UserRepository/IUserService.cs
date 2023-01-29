using EnterpriseArchitecture.Core.Utilities.Result.Abstract;
using EnterpriseArchitecture.DataTransformationObjects.Concrete.Auth;
using EnterpriseArchitecture.DataTransformationObjects.Concrete.User;

namespace EnterpriseArchitecture.Business.Repositories.UserRepository;

public interface IUserService
{
    IResult Add(RegisterDto registerDto);
    IDataResult<UserForListWithAllFieldsDto?> GetByEmail(string email); 
    IDataResult<List<UserForListWithOnlyIdDto>> GetAllUsersWithId();
    IDataResult<List<UserForListDto>> GetAllUsers();
    IDataResult<UserForListDto> FindById(Guid id);
    IDataResult<UserForListWithAllFieldsDto> FindByIdWithAllFields(Guid id);
    IResult RemoveById(Guid id);
    IResult Update(UserForUpdateDto userForUpdateDto);
    IResult ChangePassword(UserForChangePasswordDto userForChangePasswordDto);
}