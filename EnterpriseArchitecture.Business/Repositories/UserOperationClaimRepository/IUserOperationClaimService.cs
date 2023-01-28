using EnterpriseArchitecture.Core.Utilities.Result.Abstract;
using EnterpriseArchitecture.DataTransformationObjects.Concrete.UserOperationClaim;

namespace EnterpriseArchitecture.Business.Repositories.UserOperationClaimRepository;

public interface IUserOperationClaimService
{
    IResult Add(UserOperationClaimForAddDto userOperationClaimForAddDto);
    IResult Update(UserOperationClaimForUpdateDto userOperationClaimForUpdateDto);
    IResult Delete(Guid Id);
    IDataResult<UserOperationClaimForListDto> GetById(Guid Id);
    IDataResult<List<UserOperationClaimForListDto>> GetAll();
}