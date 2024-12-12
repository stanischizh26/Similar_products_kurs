using Microsoft.AspNetCore.Mvc;
﻿using MediatR;
using Similar_products.Application.Dtos;
using Similar_products.Application.Requests.Queries;
using Similar_products.Application.Requests.Commands;
using Microsoft.AspNetCore.Authorization;

namespace Similar_products.Web.Controllers;

[Route("api/products")]
[Authorize]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? name = null)
    {
        if (page < 1 || pageSize < 1)
        {
            return BadRequest("Номер страницы или листов меньше 1");
        }

        var query = new GetProductsQuery(page, pageSize, name);
        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var product = await _mediator.Send(new GetProductByIdQuery(id));

        if (product is null)
        {
            return NotFound($"Product with id {id} is not found.");
        }
        
        return Ok(product);
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Create([FromBody] ProductForCreationDto? product)
    {
        if (product is null)
        {
            return BadRequest("Object for creation is null");
        }

        await _mediator.Send(new CreateProductCommand(product));

        return CreatedAtAction(nameof(Create), product);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody]/*[Bind("Id,Name,Characteristics,Unit,Photo")]*/ ProductForUpdateDto? product)
    {
        if (product is null)
        {
            return BadRequest("Object for update is null");
        }

        var isEntityFound = await _mediator.Send(new UpdateProductCommand(product));

        if (!isEntityFound)
        {
            return NotFound($"Product with id {id} is not found.");
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var isEntityFound = await _mediator.Send(new DeleteProductCommand(id));

        if (!isEntityFound)
        {
            return NotFound($"Product with id {id} is not found.");
        }

        return NoContent();
    }

    [HttpGet("productTypes")]
    public async Task<IActionResult> GetGenders([FromQuery] string? name = null)
    {
        var result = await _mediator.Send(new GetProductTypesAllQuery(name));

        return Ok(result);
    }
}
