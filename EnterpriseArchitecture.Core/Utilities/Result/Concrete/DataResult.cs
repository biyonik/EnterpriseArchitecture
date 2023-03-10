using EnterpriseArchitecture.Core.Utilities.Result.Abstract;

namespace EnterpriseArchitecture.Core.Utilities.Result.Concrete;

public class DataResult<T> : Result, IDataResult<T>
{
    public T Data { get; set; }
    
    public DataResult(T data, bool success) : base(success)
    {
        Data = data;
    }

    public DataResult(T data, bool success, string? message) : base(success, message)
    {
        Data = data;
    }

    public DataResult()
    {
    }
}