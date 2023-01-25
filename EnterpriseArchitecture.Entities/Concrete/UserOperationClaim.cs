using System.ComponentModel.DataAnnotations.Schema;
using EnterpriseArchitecture.Entities.Abstract;

namespace EnterpriseArchitecture.Entities.Concrete;

public sealed class UserOperationClaim: IEntityWithId<Guid>
{
    public Guid Id { get; set; }
    
    [ForeignKey("User")]
    public Guid UserId { get; set; }
    public User User { get; set; }

    [ForeignKey("OperationClaim")]
    public Guid OperationClaimId { get; set; }
    public OperationClaim OperationClaim { get; set; }
}