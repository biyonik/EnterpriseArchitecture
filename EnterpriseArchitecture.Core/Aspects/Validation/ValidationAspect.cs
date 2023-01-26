using Castle.DynamicProxy;
using EnterpriseArchitecture.Core.CrossCuttingConcerns.Validation;
using EnterpriseArchitecture.Core.Utilities.Interceptors;
using FluentValidation;

namespace EnterpriseArchitecture.Core.Aspects.Validation;

public class ValidationAspect: MethodInterception
{
    private Type _validatorType;

    public ValidationAspect(Type validatorType)
    {
        _validatorType = validatorType;
    }

    protected override void OnBefore(IInvocation invocation)
    {
        IValidator validator = (IValidator)Activator.CreateInstance(_validatorType)!;
        Type entityType = _validatorType.BaseType!.GetGenericArguments()[0];
        IEnumerable<object> entities = invocation.Arguments.Where(t => t.GetType() == entityType);
        foreach (var entity in entities)
        {
            ValidationTool.Validate(validator, entity);
        }
    }
}