using EnterpriseArchitecture.Business.Repositories.UserOperationClaimRepository;
using EnterpriseArchitecture.DataTransformationObjects.Concrete.UserOperationClaim;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseArchitecture.API.Controllers;

public class UserOperationClaimsController: BaseApiController
{
    private readonly IUserOperationClaimService _userOperationClaimService;

    public UserOperationClaimsController(IUserOperationClaimService userOperationClaimService)
    {
        _userOperationClaimService = userOperationClaimService;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        return await HandleResult(_userOperationClaimService.GetById(id));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return await HandleResult(_userOperationClaimService.GetAll());
    }

    [HttpPost]
    public async Task<IActionResult> Add(UserOperationClaimForAddDto userOperationClaimForAddDto)
    {
        return await HandleResult(_userOperationClaimService.Add(userOperationClaimForAddDto));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UserOperationClaimForUpdateDto userOperationClaimForUpdateDto)
    {
        return await HandleResult(_userOperationClaimService.Update(userOperationClaimForUpdateDto));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        return await HandleResult(_userOperationClaimService.Delete(id));
    }
}