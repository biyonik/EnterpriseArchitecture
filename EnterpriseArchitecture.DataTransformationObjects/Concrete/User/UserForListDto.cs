using EnterpriseArchitecture.DataTransformationObjects.Abstract;

namespace EnterpriseArchitecture.DataTransformationObjects.Concrete.User;

public class UserDto: IDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string? ImageUrl { get; set; }
}