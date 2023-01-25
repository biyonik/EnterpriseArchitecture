using EnterpriseArchitecture.Core.Utilities.Result.Abstract;
using EnterpriseArchitecture.DataTransformationObjects.Concrete.Auth;
using EnterpriseArchitecture.Entities.Concrete;

namespace EnterpriseArchitecture.Business.Abstract;

public interface IAuthService
{
    IResult Register(RegisterDto registerDto);
    IDataResult<User> Login(LoginDto loginDto);
}