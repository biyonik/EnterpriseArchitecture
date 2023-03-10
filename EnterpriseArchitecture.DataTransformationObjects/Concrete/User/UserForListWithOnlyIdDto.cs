using EnterpriseArchitecture.DataTransformationObjects.Abstract;

namespace EnterpriseArchitecture.DataTransformationObjects.Concrete.User;

public class UserForListWithOnlyIdDto: UserForListDto, IDtoWithId<Guid>
{
    public Guid Id { get; set; }
}