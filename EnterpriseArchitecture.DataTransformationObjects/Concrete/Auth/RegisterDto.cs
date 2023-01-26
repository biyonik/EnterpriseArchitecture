using EnterpriseArchitecture.DataTransformationObjects.Abstract;
using Microsoft.AspNetCore.Http;

namespace EnterpriseArchitecture.DataTransformationObjects.Concrete.Auth;

public class RegisterDto: IDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public IFormFile? ImageFile { get; set; }
}