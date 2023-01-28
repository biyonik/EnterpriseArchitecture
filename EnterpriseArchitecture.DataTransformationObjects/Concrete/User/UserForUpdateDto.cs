using EnterpriseArchitecture.DataTransformationObjects.Abstract;
using Microsoft.AspNetCore.Http;

namespace EnterpriseArchitecture.DataTransformationObjects.Concrete.User;

public class UserForUpdateDto: IDtoWithId<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public IFormFile? Image { get; set; }
}