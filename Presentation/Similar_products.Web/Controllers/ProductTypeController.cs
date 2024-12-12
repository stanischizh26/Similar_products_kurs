using Microsoft.AspNetCore.Mvc;
﻿using MediatR;
using Similar_products.Application.Dtos;
using Similar_products.Application.Requests.Queries;
using Similar_products.Application.Requests.Commands;
using Microsoft.AspNetCore.Authorization;

namespace Similar_products.Web.Controllers;

[Route("api/productTypes")]
[Authorize]
[ApiController]
public class ProductTypeController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductTypeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int page = 1, [FromQuery] int countPage = 10, [FromQuery] string? name = null)
    {
        var productTypes = await _mediator.Send(new GetProductTypesQuery(page, countPage, name));

        return Ok(productTypes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var productType = await _mediator.Send(new GetProductTypeByIdQuery(id));

        if (productType is null)
        {
            return NotFound($"ProductType with id {id} is not found.");
        }
        
        return Ok(productType);
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Create([FromBody] ProductTypeForCreationDto? productType)
    {
        if (productType is null)
        {
            return BadRequest("Object for creation is null");
        }

        await _mediator.Send(new CreateProductTypeCommand(productType));

        return CreatedAtAction(nameof(Create), productType);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] ProductTypeForUpdateDto? productType)
    {
        if (productType is null)
        {
            return BadRequest("Object for update is null");
        }

        var isEntityFound = await _mediator.Send(new UpdateProductTypeCommand(productType));

        if (!isEntityFound)
        {
            return NotFound($"ProductType with id {id} is not found.");
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var isEntityFound = await _mediator.Send(new DeleteProductTypeCommand(id));

        if (!isEntityFound)
        {
            return NotFound($"ProductType with id {id} is not found.");
        }

        return NoContent();
    }
}
