using EnterpriseArchitecture.DataTransformationObjects.Abstract;

namespace EnterpriseArchitecture.DataTransformationObjects.Concrete.Auth;

public class RegisterDto: IDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string? ImageUrl { get; set; }
    public string Password { get; set; }
}