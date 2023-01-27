using EnterpriseArchitecture.DataAccess.Repositories.OperationClaimRepository;

namespace EnterpriseArchitecture.Business.Repositories.OperationClaimRepository;

public class OperationClaimManager: IOperationClaimService
{
    private readonly IOperationClaimDal _operationClaimDal;

    public OperationClaimManager(IOperationClaimDal operationClaimDal)
    {
        _operationClaimDal = operationClaimDal;
    }
}