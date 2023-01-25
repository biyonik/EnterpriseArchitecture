using FluentValidation;
using FluentValidation.Results;

namespace EnterpriseArchitecture.Core.CrossCuttingConcerns.Validation;

public class ValidationTool
{
    public static void Validate(IValidator validator, object entity)
    {
        ValidationContext<object> context = new ValidationContext<object>(entity);
        ValidationResult validationResult = validator.Validate(context);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
    }
}