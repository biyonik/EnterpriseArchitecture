using Castle.DynamicProxy;

namespace EnterpriseArchitecture.Core.Utilities.Interceptors;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public abstract class MethodInterceptionBaseAttribute: Attribute
{
    public virtual void Intercept(IInvocation inInvocation)
    {
        
    }
}