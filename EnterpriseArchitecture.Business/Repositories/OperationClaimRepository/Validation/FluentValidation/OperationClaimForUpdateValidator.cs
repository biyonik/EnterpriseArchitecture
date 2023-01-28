using EnterpriseArchitecture.DataTransformationObjects.Concrete.OperationClaim;
using FluentValidation;

namespace EnterpriseArchitecture.Business.Repositories.OperationClaimRepository.Validation.FluentValidation;

public class OperationClaimForUpdateValidator: AbstractValidator<OperationClaimForUpdateDto>
{
    public OperationClaimForUpdateValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Yetki Id boş bırakılamaz!")
            .NotNull().WithMessage("Yetki Id boş bırakılamaz!");
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Yetki adı boş bırakılamaz!")
            .NotNull().WithMessage("Yetki adı boş bırakılamaz!");
    }
}