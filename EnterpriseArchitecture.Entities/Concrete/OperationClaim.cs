using EnterpriseArchitecture.Entities.Abstract;

namespace EnterpriseArchitecture.Entities.Concrete;

public class OperationClaim: IEntityWithId<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public virtual UserOperationClaim UserOperationClaim { get; set; }
}