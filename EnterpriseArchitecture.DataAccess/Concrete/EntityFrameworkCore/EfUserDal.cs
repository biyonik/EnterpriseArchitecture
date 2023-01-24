using EnterpriseArchitecture.Core.DataAccess.EntityFramework;
using EnterpriseArchitecture.DataAccess.Abstract;
using EnterpriseArchitecture.DataAccess.Concrete.EntityFrameworkCore.Contexts;
using EnterpriseArchitecture.Entities.Concrete;

namespace EnterpriseArchitecture.DataAccess.Concrete.EntityFrameworkCore;

public class EfUserDal: EfEntityRepositoryBase<User, Guid, AppDbContext>, IUserDal
{
    
}