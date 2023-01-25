using EnterpriseArchitecture.Entities.Abstract;

namespace EnterpriseArchitecture.Entities.Concrete;

public sealed class OperationClaim: IEntityWithId<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}