namespace EnterpriseArchitecture.Core.Utilities.Result.Abstract;

public interface IResult
{
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
}