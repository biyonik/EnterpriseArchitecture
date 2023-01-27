using EnterpriseArchitecture.DataAccess.Repositories.UserOperationClaimRepository;

namespace EnterpriseArchitecture.Business.Repositories.UserOperationClaimRepository;

public class UserOperationClaimManager: IUserOperationClaimService
{
    private readonly IUserOperationClaimDal _userOperationClaimDal;

    public UserOperationClaimManager(IUserOperationClaimDal userOperationClaimDal)
    {
        _userOperationClaimDal = userOperationClaimDal;
    }
}