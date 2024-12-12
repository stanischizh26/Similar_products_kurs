using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Similar_products.Application.Dtos;
using System.Xml.Linq;

namespace Similar_products.Application.Requests.Queries;

public record GetProductTypesQuery :IRequest<PageResult<ProductTypeDto>>
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public string? Name { get; set; }

    public GetProductTypesQuery(int page, int pageSize, string name)
    {
        Page = page;
        PageSize = pageSize;
        Name = name;
    }
}
