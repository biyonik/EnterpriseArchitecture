using EnterpriseArchitecture.DataTransformationObjects.Abstract;

namespace EnterpriseArchitecture.DataTransformationObjects.Concrete.Auth;

public class LoginDto: IDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}