using EnterpriseArchitecture.DataTransformationObjects.Abstract;
using Microsoft.AspNetCore.Http;

namespace EnterpriseArchitecture.DataTransformationObjects.Concrete.User;

public class UserForAddDto: IDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public IFormFile? Image { get; set; }
    public string Password { get; set; }
}