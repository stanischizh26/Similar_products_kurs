using MediatR;
using Similar_products.Application.Dtos;

namespace Similar_products.Application.Requests.Queries;

public record GetSalesPlansQuery : IRequest<PageResult<SalesPlanDto>>
{
    public int Page { get; }
    public int PageSize { get; }
    public string? Name { get; }
    public GetSalesPlansQuery(int page, int pageSize, string? name)
    {
        Page = page;
        PageSize = pageSize;
        Name = name;
    }
}
