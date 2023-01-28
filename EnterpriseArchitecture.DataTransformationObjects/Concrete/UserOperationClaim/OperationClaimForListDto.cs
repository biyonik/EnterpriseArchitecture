using EnterpriseArchitecture.DataTransformationObjects.Abstract;

namespace EnterpriseArchitecture.DataTransformationObjects.Concrete.UserOperationClaim;

public class UserOperationClaimForListDto: IDtoWithId<Guid>
{
    public Guid Id { get; set; }
    public Entities.Concrete.User User { get; set; }
    public Entities.Concrete.OperationClaim OperationClaim { get; set; }
}