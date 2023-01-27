using EnterpriseArchitecture.Core.DataAccess;
using EnterpriseArchitecture.Entities.Concrete;

namespace EnterpriseArchitecture.DataAccess.Repositories.UserRepository;

public interface IUserDal: IEntityRepository<User, Guid>
{
    
}