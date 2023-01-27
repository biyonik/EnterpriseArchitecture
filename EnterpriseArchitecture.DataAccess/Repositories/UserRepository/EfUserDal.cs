using EnterpriseArchitecture.Core.DataAccess.EntityFramework;
using EnterpriseArchitecture.DataAccess.Context.EntityFrameworkCore;
using EnterpriseArchitecture.Entities.Concrete;

namespace EnterpriseArchitecture.DataAccess.Repositories.UserRepository;

public class EfUserDal: EfEntityRepositoryBase<User, Guid, AppDbContext>, IUserDal
{
    
}