using EnterpriseArchitecture.DataTransformationObjects.Concrete.OperationClaim;
using EnterpriseArchitecture.Entities.Concrete;
using FluentValidation;

namespace EnterpriseArchitecture.Business.Repositories.OperationClaimRepository.Validation.FluentValidation;

public class OperationClaimForAddValidator: AbstractValidator<OperationClaimForAddDto>
{
    public OperationClaimForAddValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Yetki adı boş bırakılamaz!")
            .NotNull().WithMessage("Yetki adı boş bırakılamaz!");
    }
}