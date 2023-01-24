using EnterpriseArchitecture.Business.Abstract;
using EnterpriseArchitecture.DataAccess.Abstract;

namespace EnterpriseArchitecture.Business.Concrete;

public class OperationClaimManager: IOperationClaimService
{
    private readonly IOperationClaimDal _operationClaimDal;

    public OperationClaimManager(IOperationClaimDal operationClaimDal)
    {
        _operationClaimDal = operationClaimDal;
    }
}