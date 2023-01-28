using EnterpriseArchitecture.DataTransformationObjects.Abstract;

namespace EnterpriseArchitecture.DataTransformationObjects.Concrete.OperationClaim;

public class OperationClaimForUpdateDto: IDtoWithId<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}