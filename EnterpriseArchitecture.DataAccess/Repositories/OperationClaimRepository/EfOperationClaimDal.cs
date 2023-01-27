using EnterpriseArchitecture.Core.DataAccess.EntityFramework;
using EnterpriseArchitecture.DataAccess.Context.EntityFrameworkCore;
using EnterpriseArchitecture.Entities.Concrete;

namespace EnterpriseArchitecture.DataAccess.Repositories.OperationClaimRepository;

public class EfOperationClaimDal: EfEntityRepositoryBase<OperationClaim, Guid, AppDbContext>, IOperationClaimDal
{
    
}