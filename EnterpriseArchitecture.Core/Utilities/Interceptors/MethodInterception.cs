using Castle.DynamicProxy;

namespace EnterpriseArchitecture.Core.Utilities.Interceptors;

public class MethodInterception: MethodInterceptionBaseAttribute
{
    protected virtual void OnBefore(IInvocation invocation) { }
    protected virtual void OnAfter(IInvocation invocation) { }
    protected virtual void OnException(IInvocation invocation, Exception exception) { }
    protected virtual void OnSuccess(IInvocation invocation) { }

    public override void Intercept(IInvocation innvocation)
    {
        var isSuccess = true;
        OnBefore(innvocation);

        try
        {
            innvocation.Proceed();
        }
        catch (Exception ex)
        {
            isSuccess = false;
            OnException(innvocation, ex);
            throw;
        }
        finally
        {
            if (isSuccess)
            {
                OnSuccess(innvocation);
            }
        }
        OnAfter(innvocation);
    }
}