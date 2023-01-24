using EnterpriseArchitecture.Core.DataAccess;
using EnterpriseArchitecture.Entities.Concrete;

namespace EnterpriseArchitecture.DataAccess.Abstract;

public interface IOperationClaimDal : IEntityRepository<OperationClaim, Guid>
{
    
}