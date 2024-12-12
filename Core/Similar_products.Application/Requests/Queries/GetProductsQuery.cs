using MediatR;
using Similar_products.Application.Dtos;

namespace Similar_products.Application.Requests.Queries;

public record GetProductsQuery : IRequest<PageResult<ProductDto>>
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public string? Name { get; set; }

    public GetProductsQuery(int page, int pageSize, string name)
    {
        Page = page;
        PageSize = pageSize;
        Name = name;
    }
}
