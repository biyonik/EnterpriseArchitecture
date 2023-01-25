using EnterpriseArchitecture.Core.Utilities.Result.Abstract;

namespace EnterpriseArchitecture.Core.Utilities.Result.Concrete;

public class Result: IResult
{
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
    
    public Result(bool success)
    {
        IsSuccess = success;
    }
    public Result(bool success, string? message): this(success)
    {
        Message = message;
    }
}