using EnterpriseArchitecture.Core.DataAccess.EntityFramework;
using EnterpriseArchitecture.DataAccess.Context.EntityFrameworkCore;
using EnterpriseArchitecture.Entities.Concrete;

namespace EnterpriseArchitecture.DataAccess.Repositories.UserOperationClaimRepository;

public class EfUserOperationClaimDal: EfEntityRepositoryBase<UserOperationClaim, Guid, AppDbContext>, IUserOperationClaimDal
{
    
}