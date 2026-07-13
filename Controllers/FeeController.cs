using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SMS.DTOS.FeeDtos;
using SMS.Models;
using StudentManagementSystem.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

[ApiController]
[Route("api/v1/fees")]
[ApiVersion("1.0")]
public class FeeController : ControllerBase
{
    private readonly IFeeService _service;

    public FeeController(IFeeService service)
    {
        _service = service;
    }
    //================================================
    [HttpPost]

    [SwaggerOperation(Summary = "Create Fee",Description = "Generate fee record")]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult>CreateFee(CreateFeeRequestDTO dto)
    {
        await _service.CreateFeeAsync(dto);
        return Ok();
    }
    //=========================================================

    [HttpPost("{id}/pay")]
    [Authorize(Roles = "ADMIN,CASHIER")]
    [SwaggerOperation( Summary = "Pay Fee",Description = "Record fee payment")]
    public async Task<IActionResult> PayFee(int id,PayFeeRequestDTO dto)
    {
        await _service.PayFeeAsync(id, dto);
        return Ok();
    }
}