using EnterpriseArchitecture.Business.Repositories.OperationClaimRepository.Constants;
using EnterpriseArchitecture.Business.Repositories.OperationClaimRepository.Validation.FluentValidation;
using EnterpriseArchitecture.Core.Aspects.Validation;
using EnterpriseArchitecture.Core.Utilities.Result.Abstract;
using EnterpriseArchitecture.Core.Utilities.Result.Concrete;
using EnterpriseArchitecture.DataAccess.Repositories.OperationClaimRepository;
using EnterpriseArchitecture.DataTransformationObjects.Concrete.OperationClaim;
using EnterpriseArchitecture.Entities.Concrete;

namespace EnterpriseArchitecture.Business.Repositories.OperationClaimRepository;

public class OperationClaimManager: IOperationClaimService
{
    private readonly IOperationClaimDal _operationClaimDal;

    public OperationClaimManager(IOperationClaimDal operationClaimDal)
    {
        _operationClaimDal = operationClaimDal;
    }

    [ValidationAspect(typeof(OperationClaimValidator))]
    public IResult Add(OperationClaimForAddDto operationClaimForAddDto)
    {
        var operationClaim = new OperationClaim
        {
            Id = Guid.NewGuid(),
            Name = operationClaimForAddDto.Name
        };
        var result = _operationClaimDal.Add(operationClaim);
        return result
            ? new SuccessResult(OperationClaimMessage.AddNewOperationClaimSuccess)
            : new ErrorResult(OperationClaimMessage.AddnewOperationClaimFailed);
    }

    [ValidationAspect(typeof(OperationClaimValidator))]
    public IResult Update(OperationClaimForUpdateDto operationClaimForUpdateDto)
    {
        var operationClaim = new OperationClaim
        {
            Id = operationClaimForUpdateDto.Id,
            Name = operationClaimForUpdateDto.Name
        };
        var result = _operationClaimDal.Update(operationClaim);
        return result
            ? new SuccessResult(OperationClaimMessage.UpdateOperationClaimSuccess)
            : new ErrorResult(OperationClaimMessage.UpdateOperationClaimFailed);
    }

    public IResult Delete(Guid Id)
    {
        var operationClaim = _operationClaimDal.GetById(Id);
        if (operationClaim == null) return new ErrorResult(OperationClaimMessage.OperationClaimNotFound);
        var result = _operationClaimDal.Delete(operationClaim);
        return result
            ? new SuccessResult(OperationClaimMessage.DeleteOperationClaimSuccess)
            : new ErrorResult(OperationClaimMessage.DeleteOperationClaimFailed);
    }

    public IDataResult<OperationClaimForListDto> GetById(Guid Id)
    {
        var operationClaim = _operationClaimDal.GetById(Id);
        var operationClaimForListDto = new OperationClaimForListDto
        {
            Id = operationClaim.Id,
            Name = operationClaim.Name
        };
        return operationClaim != null
            ? new SuccessDataResult<OperationClaimForListDto>(operationClaimForListDto)
            : new ErrorDataResult<OperationClaimForListDto>(OperationClaimMessage.OperationClaimNotFound);
    }

    public IDataResult<List<OperationClaimForListDto>> GetAll()
    {
        var operationClaims = _operationClaimDal.GetAll();
        if (operationClaims.Count == 0)
            return new ErrorDataResult<List<OperationClaimForListDto>>(OperationClaimMessage.OperationClaimNotFound);
        
        var operationClaimsForListDtos = new List<OperationClaimForListDto>();
        foreach (var operationClaim in operationClaims)
        {
            var opClaim = new OperationClaimForListDto
            {
                Id = operationClaim.Id,
                Name = operationClaim.Name
            };
            operationClaimsForListDtos.Add(opClaim);
        }

        return new SuccessDataResult<List<OperationClaimForListDto>>(operationClaimsForListDtos);
    }
}