using EnterpriseArchitecture.DataTransformationObjects.Concrete.Auth;
using EnterpriseArchitecture.Entities.Concrete;

namespace EnterpriseArchitecture.Business.Abstract;

public interface IAuthService
{
    bool Register(RegisterDto registerDto);
    User? Login(LoginDto loginDto);
}