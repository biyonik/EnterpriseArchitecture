using EnterpriseArchitecture.Core.DataAccess;
using EnterpriseArchitecture.Entities.Concrete;

namespace EnterpriseArchitecture.DataAccess.Repositories.UserOperationClaimRepository;

public interface IUserOperationClaimDal: IEntityRepository<UserOperationClaim, Guid>
{
    
}