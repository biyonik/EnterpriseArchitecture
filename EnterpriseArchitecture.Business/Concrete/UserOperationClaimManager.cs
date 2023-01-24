using EnterpriseArchitecture.Business.Abstract;
using EnterpriseArchitecture.DataAccess.Abstract;

namespace EnterpriseArchitecture.Business.Concrete;

public class UserOperationClaimManager: IUserOperationClaimService
{
    private readonly IUserOperationClaimDal _userOperationClaimDal;

    public UserOperationClaimManager(IUserOperationClaimDal userOperationClaimDal)
    {
        _userOperationClaimDal = userOperationClaimDal;
    }
}