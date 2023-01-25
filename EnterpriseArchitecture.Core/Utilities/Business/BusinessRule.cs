using EnterpriseArchitecture.Core.Utilities.Result.Abstract;

namespace EnterpriseArchitecture.Core.Utilities.Business;

public static class BusinessRule
{
    public static IResult? Run(params IResult?[] logics)
    {
        return logics.FirstOrDefault((IResult? logic) => logic is { IsSuccess: false });
    }
}