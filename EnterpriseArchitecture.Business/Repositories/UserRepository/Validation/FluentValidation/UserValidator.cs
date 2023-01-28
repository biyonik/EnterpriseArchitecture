using EnterpriseArchitecture.DataTransformationObjects.Concrete.Auth;
using FluentValidation;

namespace EnterpriseArchitecture.Business.Repositories.UserRepository.Validation.FluentValidation;

public class UserValidator: AbstractValidator<RegisterDto>
{
    public UserValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email boş bırakılamaz!")
            .NotNull().WithMessage("Email boş bırakılamaz!")
            .EmailAddress().WithMessage("Lütfen geçerli bir e-posta adresi giriniz!");
        
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Tam isim boş bırakılamaz!")
            .NotNull().WithMessage("Tam isim boş bırakılamaz!");
    }
}