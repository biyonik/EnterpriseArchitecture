using EnterpriseArchitecture.Business.Repositories.OperationClaimRepository;
using EnterpriseArchitecture.DataTransformationObjects.Concrete.OperationClaim;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseArchitecture.API.Controllers;

public class OperationClaimsController: BaseApiController
{
    private readonly IOperationClaimService _operationClaimService;

    public OperationClaimsController(IOperationClaimService operationClaimService)
    {
        _operationClaimService = operationClaimService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        return await HandleResult(_operationClaimService.GetById(id));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return await HandleResult(_operationClaimService.GetAll());
    }

    [HttpPost]
    public async Task<IActionResult> Add(OperationClaimForAddDto operationClaimForAddDto)
    {
        return await HandleResult(_operationClaimService.Add(operationClaimForAddDto));
    }

    [HttpPut]
    public async Task<IActionResult> Update(OperationClaimForUpdateDto operationClaimForUpdateDto)
    {
        return await HandleResult(_operationClaimService.Update(operationClaimForUpdateDto));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        return await HandleResult(_operationClaimService.Delete(id));
    }
}