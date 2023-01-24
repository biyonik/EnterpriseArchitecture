using EnterpriseArchitecture.Core.DataAccess;
using EnterpriseArchitecture.Entities.Concrete;

namespace EnterpriseArchitecture.DataAccess.Abstract;

public interface IUserOperationClaimDal: IEntityRepository<UserOperationClaim, Guid>
{
    
}