using Microsoft.AspNetCore.Mvc;
﻿using MediatR;
using Similar_products.Application.Dtos;
using Similar_products.Application.Requests.Queries;
using Similar_products.Application.Requests.Commands;
using Microsoft.AspNetCore.Authorization;

namespace Similar_products.Web.Controllers;

[Route("api/salesPlans")]
[Authorize]
[ApiController]
public class SalesPlanController : ControllerBase
{
    private readonly IMediator _mediator;

    public SalesPlanController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? name = null)
    {
        var salesPlans = await _mediator.Send(new GetSalesPlansQuery(page, pageSize, name));

        return Ok(salesPlans);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var salesPlan = await _mediator.Send(new GetSalesPlanByIdQuery(id));

        if (salesPlan is null)
        {
            return NotFound($"SalesPlan with id {id} is not found.");
        }
        
        return Ok(salesPlan);
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Create([FromBody] SalesPlanForCreationDto? salesPlan)
    {
        if (salesPlan is null)
        {
            return BadRequest("Object for creation is null");
        }

        await _mediator.Send(new CreateSalesPlanCommand(salesPlan));

        return CreatedAtAction(nameof(Create), salesPlan);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] SalesPlanForUpdateDto? salesPlan)
    {
        if (salesPlan is null)
        {
            return BadRequest("Object for update is null");
        }

        var isEntityFound = await _mediator.Send(new UpdateSalesPlanCommand(salesPlan));

        if (!isEntityFound)
        {
            return NotFound($"SalesPlan with id {id} is not found.");
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var isEntityFound = await _mediator.Send(new DeleteSalesPlanCommand(id));

        if (!isEntityFound)
        {
            return NotFound($"SalesPlan with id {id} is not found.");
        }

        return NoContent();
    }
    [HttpGet("products")]
    public async Task<IActionResult> GetProducts([FromQuery] string? name = null)
    {
        var result = await _mediator.Send(new GetProductsAllQuery());

        return Ok(result);
    }

    [HttpGet("enterprises")]
    public async Task<IActionResult> GetEnterprises([FromQuery] string? name = null)
    {
        var result = await _mediator.Send(new GetEnterprisesAllQuery());

        return Ok(result);
    }
}
