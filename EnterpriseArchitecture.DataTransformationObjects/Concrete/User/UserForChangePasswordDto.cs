using EnterpriseArchitecture.DataTransformationObjects.Abstract;

namespace EnterpriseArchitecture.DataTransformationObjects.Concrete.User;

public sealed class UserForChangePasswordDto: IDtoWithId<Guid>
{
    public Guid Id { get; set; }
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
}