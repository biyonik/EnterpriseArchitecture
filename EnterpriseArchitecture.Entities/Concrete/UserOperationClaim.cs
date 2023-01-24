using EnterpriseArchitecture.Entities.Abstract;

namespace EnterpriseArchitecture.Entities.Concrete;

public class UserOperationClaim: IEntityWithId<Guid>
{
    public Guid Id { get; set; }
    
    public virtual User User { get; set; }
    public Guid UserId { get; set; }

    public virtual OperationClaim OperationClaim { get; set; }
    public Guid OperationClaimId { get; set; }
}