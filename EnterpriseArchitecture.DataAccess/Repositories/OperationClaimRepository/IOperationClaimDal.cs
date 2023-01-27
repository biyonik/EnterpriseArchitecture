using EnterpriseArchitecture.Core.DataAccess;
using EnterpriseArchitecture.Entities.Concrete;

namespace EnterpriseArchitecture.DataAccess.Repositories.OperationClaimRepository;

public interface IOperationClaimDal : IEntityRepository<OperationClaim, Guid>
{
    
}