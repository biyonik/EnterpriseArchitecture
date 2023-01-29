using EnterpriseArchitecture.Business.Repositories.OperationClaimRepository;
using EnterpriseArchitecture.Business.Repositories.OperationClaimRepository.Constants;
using EnterpriseArchitecture.Business.Repositories.UserOperationClaimRepository.Constants;
using EnterpriseArchitecture.Business.Repositories.UserOperationClaimRepository.Validation.FluentValidation;
using EnterpriseArchitecture.Business.Repositories.UserRepository;
using EnterpriseArchitecture.Business.Repositories.UserRepository.Constants;
using EnterpriseArchitecture.Core.Aspects.Validation;
using EnterpriseArchitecture.Core.Utilities.Business;
using EnterpriseArchitecture.Core.Utilities.Result.Abstract;
using EnterpriseArchitecture.Core.Utilities.Result.Concrete;
using EnterpriseArchitecture.DataAccess.Repositories.UserOperationClaimRepository;
using EnterpriseArchitecture.DataTransformationObjects.Concrete.OperationClaim;
using EnterpriseArchitecture.DataTransformationObjects.Concrete.User;
using EnterpriseArchitecture.DataTransformationObjects.Concrete.UserOperationClaim;
using EnterpriseArchitecture.Entities.Concrete;

namespace EnterpriseArchitecture.Business.Repositories.UserOperationClaimRepository;

public class UserOperationClaimManager: IUserOperationClaimService
{
    private readonly IUserOperationClaimDal _userOperationClaimDal;
    private readonly IOperationClaimService _operationClaimService;
    private readonly IUserService _userService;

    public UserOperationClaimManager(IUserOperationClaimDal userOperationClaimDal, IOperationClaimService operationClaimService, IUserService userService)
    {
        _userOperationClaimDal = userOperationClaimDal;
        _operationClaimService = operationClaimService;
        _userService = userService;
    }

    [ValidationAspect(typeof(UserOperationClaimValidator))]
    public IResult Add(UserOperationClaimForAddDto userOperationClaimForAddDto)
    {
        IResult? ruleResult = BusinessRule.Run(
            IsOperationClaimSet(userOperationClaimForAddDto),
            IsOperationClaimExist(userOperationClaimForAddDto.OperationClaimId),
            IsUserExist(userOperationClaimForAddDto.UserId)
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
        var userForListDto = _userService.FindByIdWithAllFields(userOperationClaim.UserId).Data;
        var operationClaimForListDto = _operationClaimService.GetById(userOperationClaim.OperationClaimId).Data;

        var user = new User
        {
            Id = userForListDto.Id,
            Email = userForListDto.Email,
            Name = userForListDto.Name,
            ImageUrl = userForListDto.ImageUrl
        };

        var operationClaim = new OperationClaim
        {
            Id = operationClaimForListDto.Id,
            Name = operationClaimForListDto.Name
        };
        
        var userOperationClaimForListDto = new UserOperationClaimForListDto
        {
            Id = userOperationClaim.Id,
            User = user,
            OperationClaim = operationClaim
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
            var userForListDto = _userService.FindByIdWithAllFields(userOperationClaim.UserId).Data;
            var operationClaimForListDto = _operationClaimService.GetById(userOperationClaim.OperationClaimId).Data;

            var user = new User
            {
                Id = userForListDto.Id,
                Email = userForListDto.Email,
                Name = userForListDto.Name,
                ImageUrl = userForListDto.ImageUrl
            };

            var operationClaim = new OperationClaim
            {
                Id = operationClaimForListDto.Id,
                Name = operationClaimForListDto.Name
            };
            
            var opClaim = new UserOperationClaimForListDto
            {
                Id = userOperationClaim.Id,
                User = user,
                OperationClaim = operationClaim
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
    
    private IResult IsOperationClaimExist(Guid operationClaimId)
    {
        IDataResult<OperationClaimForListDto> result = _operationClaimService.GetById(operationClaimId);
        if (result.IsSuccess == false || result.Data == null)
            return new ErrorResult(OperationClaimMessage.OperationClaimNotFound);
        return new SuccessResult();
    }
    
    private IResult IsUserExist(Guid userId)
    {
        IDataResult<UserForListDto> result = _userService.FindById(userId);
        if (result.IsSuccess == false || result.Data == null)
            return new ErrorResult(UserMessages.UserNotFound);
        return new SuccessResult();
    }
}