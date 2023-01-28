using EnterpriseArchitecture.Core.Utilities.Result.Abstract;
using EnterpriseArchitecture.DataTransformationObjects.Concrete.OperationClaim;

namespace EnterpriseArchitecture.Business.Repositories.OperationClaimRepository;

public interface IOperationClaimService
{
    IResult Add(OperationClaimForAddDto operationClaimForAddDto);
    IResult Update(OperationClaimForUpdateDto operationClaimForUpdateDto);
    IResult Delete(Guid Id);
    IDataResult<OperationClaimForListDto> GetById(Guid Id);
    IDataResult<List<OperationClaimForListDto>> GetAll();
}