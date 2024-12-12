using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Similar_products.Application.Dtos;
using System.Xml.Linq;

namespace Similar_products.Application.Requests.Queries;

public record GetProductTypesAllQuery : IRequest<IEnumerable<ProductTypeDto>>
{
    public string? Name { get; set; }

    public GetProductTypesAllQuery(string name)
    {
        Name = name;
    }
}
