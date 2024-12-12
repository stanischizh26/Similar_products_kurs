using Microsoft.AspNetCore.Mvc;
﻿using MediatR;
using Similar_products.Application.Dtos;
using Similar_products.Application.Requests.Queries;
using Similar_products.Application.Requests.Commands;
using Bogus.DataSets;
using Microsoft.AspNetCore.Authorization;

namespace Similar_products.Web.Controllers;

[Route("api/enterprises")]
[Authorize]
[ApiController]
public class EnterpriseController : ControllerBase
{
    private readonly IMediator _mediator;

    public EnterpriseController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? name = null)
    {
        var enterprises = await _mediator.Send(new GetEnterprisesQuery(page, pageSize, name));

        return Ok(enterprises);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var enterprise = await _mediator.Send(new GetEnterpriseByIdQuery(id));

        if (enterprise is null)
        {
            return NotFound($"Enterprise with id {id} is not found.");
        }
        
        return Ok(enterprise);
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Create([FromBody] EnterpriseForCreationDto? enterprise)
    {
        if (enterprise is null)
        {
            return BadRequest("Object for creation is null");
        }

        await _mediator.Send(new CreateEnterpriseCommand(enterprise));

        return CreatedAtAction(nameof(Create), enterprise);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] EnterpriseForUpdateDto? enterprise)
    {
        if (enterprise is null)
        {
            return BadRequest("Object for update is null");
        }

        var isEntityFound = await _mediator.Send(new UpdateEnterpriseCommand(enterprise));

        if (!isEntityFound)
        {
            return NotFound($"Enterprise with id {id} is not found.");
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var isEntityFound = await _mediator.Send(new DeleteEnterpriseCommand(id));

        if (!isEntityFound)
        {
            return NotFound($"Enterprise with id {id} is not found.");
        }

        return NoContent();
    }
}
