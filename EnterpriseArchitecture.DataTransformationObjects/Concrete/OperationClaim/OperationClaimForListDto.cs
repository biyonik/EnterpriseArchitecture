using EnterpriseArchitecture.DataTransformationObjects.Abstract;

namespace EnterpriseArchitecture.DataTransformationObjects.Concrete.OperationClaim;

public class OperationClaimForListDto: IDtoWithId<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}