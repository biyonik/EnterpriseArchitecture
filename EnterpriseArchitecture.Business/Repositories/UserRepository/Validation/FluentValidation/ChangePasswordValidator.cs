using EnterpriseArchitecture.DataTransformationObjects.Concrete.User;
using FluentValidation;

namespace EnterpriseArchitecture.Business.Repositories.UserRepository.Validation.FluentValidation;

public class ChangePasswordValidator: AbstractValidator<UserForChangePasswordDto>
{
    public ChangePasswordValidator()
    {
        RuleFor(x => x.CurrentPassword)
            .NotEmpty().WithMessage("Şuanki parola boş bırakılamaz!")
            .NotNull().WithMessage("Şuanki parola boş bırakılamaz!");
        
        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage("Yeni parola boş bırakılamaz!")
            .NotNull().WithMessage("Yeni parola boş bırakılamaz!")
            .MinimumLength(6).WithMessage("Yeni parola en az 6 karakter uzunluğunda olmalıdır")
            .MaximumLength(16).WithMessage("Yeni parola en fazla 16 karakter uzunluğunda olabilir!")
            .Matches(@"[A-Z]+").WithMessage("Yeni parola en az bir adet büyük harf içermelidir!.")
            .Matches(@"[a-z]+").WithMessage("Yeni parola en az bir adet küçük harf içermelidir!")
            .Matches(@"[0-9]+").WithMessage("Yeni parola en az bir adet rakam içermelidir!")
            .Matches(@"[\!\?\*\.]+").WithMessage("Yeni parola en az bir adet şu özel karakterlerden birisini içermelidir! Karakter seti: (!? *.)");
    }
}