using EnterpriseArchitecture.DataTransformationObjects.Concrete.Auth;
using EnterpriseArchitecture.Entities.Concrete;

namespace EnterpriseArchitecture.Business.Abstract;

public interface IAuthService
{
    string Register(RegisterDto registerDto);
    User? Login(LoginDto loginDto);
}