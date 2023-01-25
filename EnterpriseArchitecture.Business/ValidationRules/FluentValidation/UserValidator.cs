using EnterpriseArchitecture.DataTransformationObjects.Concrete.Auth;
using FluentValidation;

namespace EnterpriseArchitecture.Business.ValidationRules.FluentValidation;

public class UserValidator: AbstractValidator<RegisterDto>
{
    public UserValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email boş bırakılamaz!")
            .NotNull().WithMessage("Email boş bırakılamaz!");
        
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Tam isim boş bırakılamaz!")
            .NotNull().WithMessage("Tam isim boş bırakılamaz!");
        
        RuleFor(x => x.ImageUrl)
            .NotEmpty().WithMessage("Kullanıcı resmi boş bırakılamaz!")
            .NotNull().WithMessage("Kullanıcı resmi boş bırakılamaz!");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Parola boş bırakılamaz!")
            .NotNull().WithMessage("Parola boş bırakılamaz!")
            .MinimumLength(6).WithMessage("Parola en az 6 karakter uzunluğunda olmalıdır")
            .MaximumLength(16).WithMessage("Parola en fazla 16 karakter uzunluğunda olabilir!")
            .Matches(@"[A-Z]+").WithMessage("Parola en az bir adet büyük harf içermelidir!.")
            .Matches(@"[a-z]+").WithMessage("Parola en az bir adet küçük harf içermelidir!")
            .Matches(@"[0-9]+").WithMessage("Parola en az bir adet rakam içermelidir!")
            .Matches(@"[\!\?\*\.]+").WithMessage("Parola en az bir adet şu özel karakterlerden birisini içermelidir! Karakter seti: (!? *.)");
    }
}