using EnterpriseArchitecture.Entities.Concrete;
using FluentValidation;

namespace EnterpriseArchitecture.Business.Repositories.OperationClaimRepository.Validation.FluentValidation;

public class OperationClaimValidator: AbstractValidator<OperationClaim>
{
    public OperationClaimValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Yetki adı boş bırakılamaz!")
            .NotNull().WithMessage("Yetki adı boş bırakılamaz!");
    }
}