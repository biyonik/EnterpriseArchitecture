using EnterpriseArchitecture.Entities.Concrete;
using FluentValidation;

namespace EnterpriseArchitecture.Business.Repositories.UserOperationClaimRepository.Validation.FluentValidation;

public class UserOperationClaimValidator: AbstractValidator<UserOperationClaim>
{
    public UserOperationClaimValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("Yetki ataması için kullanıcı seçimi yapmalısınız")
            .NotNull().WithMessage("Yetki ataması için kullanıcı seçimi yapmalısınız");
        
        RuleFor(x => x.OperationClaimId)
            .NotEmpty().WithMessage("Yetki ataması için yetki seçimi yapmalısınız!")
            .NotNull().WithMessage("Yetki ataması için yetki seçimi yapmalısınız!");
    }
}