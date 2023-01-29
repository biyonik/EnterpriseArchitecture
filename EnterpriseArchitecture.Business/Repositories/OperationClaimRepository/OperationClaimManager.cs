using EnterpriseArchitecture.Business.Repositories.OperationClaimRepository.Constants;
using EnterpriseArchitecture.Business.Repositories.OperationClaimRepository.Validation.FluentValidation;
using EnterpriseArchitecture.Core.Aspects.Validation;
using EnterpriseArchitecture.Core.Utilities.Business;
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

    [ValidationAspect(typeof(OperationClaimForAddValidator))]
    public IResult Add(OperationClaimForAddDto operationClaimForAddDto)
    {
        IResult? businessRuleResult = BusinessRule.Run(
            IsNameAvailable(operationClaimForAddDto.Name)
        );
        
        if (businessRuleResult is { IsSuccess: false })
        {
            return new ErrorResult(businessRuleResult.Message);
        }
        
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

    [ValidationAspect(typeof(OperationClaimForUpdateValidator))]
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
        OperationClaim? operationClaim = _operationClaimDal.GetById(Id);
        if (operationClaim == null)
            return new ErrorDataResult<OperationClaimForListDto>(OperationClaimMessage.OperationClaimNotFound);
        
        var operationClaimForListDto = new OperationClaimForListDto
        {
            Id = operationClaim.Id,
            Name = operationClaim.Name
        };
        return new SuccessDataResult<OperationClaimForListDto>(operationClaimForListDto);
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

    private IResult IsNameAvailable(string name)
    {
        OperationClaim? result = _operationClaimDal.Get(p => p.Name == name);
        if (result != null) return new ErrorResult(OperationClaimMessage.NameAlreadyExist);
        return new SuccessResult();
    }
}