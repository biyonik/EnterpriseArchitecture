using EnterpriseArchitecture.DataTransformationObjects.Abstract;

namespace EnterpriseArchitecture.DataTransformationObjects.Concrete.User;

public class AddUserDto: IDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string ImageUrl { get; set; }
    public string Password { get; set; }
}