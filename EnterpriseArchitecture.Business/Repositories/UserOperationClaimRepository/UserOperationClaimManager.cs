using EnterpriseArchitecture.Business.Repositories.UserOperationClaimRepository.Constants;
using EnterpriseArchitecture.Business.Repositories.UserOperationClaimRepository.Validation.FluentValidation;
using EnterpriseArchitecture.Core.Aspects.Validation;
using EnterpriseArchitecture.Core.Utilities.Business;
using EnterpriseArchitecture.Core.Utilities.Result.Abstract;
using EnterpriseArchitecture.Core.Utilities.Result.Concrete;
using EnterpriseArchitecture.DataAccess.Repositories.UserOperationClaimRepository;
using EnterpriseArchitecture.DataTransformationObjects.Concrete.UserOperationClaim;
using EnterpriseArchitecture.Entities.Concrete;

namespace EnterpriseArchitecture.Business.Repositories.UserOperationClaimRepository;

public class UserOperationClaimManager: IUserOperationClaimService
{
    private readonly IUserOperationClaimDal _userOperationClaimDal;

    public UserOperationClaimManager(IUserOperationClaimDal userOperationClaimDal)
    {
        _userOperationClaimDal = userOperationClaimDal;
    }

    [ValidationAspect(typeof(UserOperationClaimValidator))]
    public IResult Add(UserOperationClaimForAddDto userOperationClaimForAddDto)
    {
        IResult? ruleResult = BusinessRule.Run(
            IsOperationClaimSet(userOperationClaimForAddDto)
        );
        
        if (ruleResult is { IsSuccess: false })
        {
            return new ErrorResult(ruleResult.Message);
        }
        
        var userOperationClaim = new UserOperationClaim
        {
            Id = Guid.NewGuid(),
            UserId = userOperationClaimForAddDto.UserId,
            OperationClaimId = userOperationClaimForAddDto.OperationClaimId
        };
        var result = _userOperationClaimDal.Add(userOperationClaim);
        return result
            ? new SuccessResult(UserOperationClaimMessages.AddNewUserOperationClaimSuccess)
            : new ErrorResult(UserOperationClaimMessages.AddnewUserOperationClaimFailed);
    }

    [ValidationAspect(typeof(UserOperationClaimValidator))]
    public IResult Update(UserOperationClaimForUpdateDto userOperationClaimForUpdateDto)
    {
        var userOperationClaim = new UserOperationClaim
        {
            Id = userOperationClaimForUpdateDto.Id,
            UserId = userOperationClaimForUpdateDto.UserId,
            OperationClaimId = userOperationClaimForUpdateDto.OperationClaimId
        };
        var result = _userOperationClaimDal.Update(userOperationClaim);
        return result
            ? new SuccessResult(UserOperationClaimMessages.UpdateUserOperationClaimSuccess)
            : new ErrorResult(UserOperationClaimMessages.UpdateUserOperationClaimFailed);
    }

    public IResult Delete(Guid Id)
    {
        var userOperationClaim = _userOperationClaimDal.GetById(Id);
        if (userOperationClaim == null) return new ErrorResult(UserOperationClaimMessages.UserOperationClaimNotFound);
        var result = _userOperationClaimDal.Delete(userOperationClaim);
        return result
            ? new SuccessResult(UserOperationClaimMessages.DeleteUserOperationClaimSuccess)
            : new ErrorResult(UserOperationClaimMessages.DeleteUserOperationClaimFailed);
    }

    public IDataResult<UserOperationClaimForListDto> GetById(Guid Id)
    {
        var userOperationClaim = _userOperationClaimDal.GetById(Id);
        var userOperationClaimForListDto = new UserOperationClaimForListDto
        {
            Id = userOperationClaim.Id,
            User = userOperationClaim.User,
            OperationClaim = userOperationClaim.OperationClaim
        };
        return userOperationClaim != null
            ? new SuccessDataResult<UserOperationClaimForListDto>(userOperationClaimForListDto)
            : new ErrorDataResult<UserOperationClaimForListDto>(UserOperationClaimMessages.UserOperationClaimNotFound);
    }

    public IDataResult<List<UserOperationClaimForListDto>> GetAll()
    {
        var userOperationClaims = _userOperationClaimDal.GetAll();
        if (userOperationClaims.Count == 0)
            return new ErrorDataResult<List<UserOperationClaimForListDto>>(UserOperationClaimMessages.UserOperationClaimNotFound);
        
        var userOperationClaimsForListDtos = new List<UserOperationClaimForListDto>();
        foreach (var userOperationClaim in userOperationClaims)
        {
            var opClaim = new UserOperationClaimForListDto
            {
                Id = userOperationClaim.Id,
                User = userOperationClaim.User,
                OperationClaim = userOperationClaim.OperationClaim
            };
            userOperationClaimsForListDtos.Add(opClaim);
        }

        return new SuccessDataResult<List<UserOperationClaimForListDto>>(userOperationClaimsForListDtos);
    }

    private IResult IsOperationClaimSet(UserOperationClaimForAddDto userOperationClaimForAddDto)
    {
        UserOperationClaim? result = _userOperationClaimDal.Get(x =>
            x.UserId == userOperationClaimForAddDto.UserId &&
            x.OperationClaimId == userOperationClaimForAddDto.OperationClaimId);
        if (result != null) return new ErrorResult(UserOperationClaimMessages.OperationClaimAlreadySet);
        return new SuccessResult();
    }
}