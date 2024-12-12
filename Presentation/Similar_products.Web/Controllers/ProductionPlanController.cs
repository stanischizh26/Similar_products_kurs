using Microsoft.AspNetCore.Mvc;
﻿using MediatR;
using Similar_products.Application.Dtos;
using Similar_products.Application.Requests.Queries;
using Similar_products.Application.Requests.Commands;
using Microsoft.AspNetCore.Authorization;

namespace Similar_products.Web.Controllers;

[Route("api/productionPlans")]
[Authorize]
[ApiController]
public class ProductionPlanController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductionPlanController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? name = null)
    {
        var productionPlans = await _mediator.Send(new GetProductionPlansQuery(page, pageSize, name));

        return Ok(productionPlans);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var productionPlan = await _mediator.Send(new GetProductionPlanByIdQuery(id));

        if (productionPlan is null)
        {
            return NotFound($"ProductionPlan with id {id} is not found.");
        }
        
        return Ok(productionPlan);
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Create([FromBody] ProductionPlanForCreationDto? productionPlan)
    {
        if (productionPlan is null)
        {
            return BadRequest("Object for creation is null");
        }

        await _mediator.Send(new CreateProductionPlanCommand(productionPlan));

        return CreatedAtAction(nameof(Create), productionPlan);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] ProductionPlanForUpdateDto? productionPlan)
    {
        if (productionPlan is null)
        {
            return BadRequest("Object for update is null");
        }

        var isEntityFound = await _mediator.Send(new UpdateProductionPlanCommand(productionPlan));

        if (!isEntityFound)
        {
            return NotFound($"ProductionPlan with id {id} is not found.");
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var isEntityFound = await _mediator.Send(new DeleteProductionPlanCommand(id));

        if (!isEntityFound)
        {
            return NotFound($"ProductionPlan with id {id} is not found.");
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
