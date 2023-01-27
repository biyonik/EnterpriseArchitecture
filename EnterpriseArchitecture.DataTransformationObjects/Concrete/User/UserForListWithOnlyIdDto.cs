using EnterpriseArchitecture.DataTransformationObjects.Abstract;

namespace EnterpriseArchitecture.DataTransformationObjects.Concrete.User;

public class UserWithIdDto: UserDto, IDtoWithId<Guid>
{
    public Guid Id { get; set; }
}