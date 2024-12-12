using MediatR;
using Similar_products.Application.Dtos;
using Similar_products.Application;

namespace Similar_products.Application.Requests.Queries;

public record GetUsersQuery : IRequest<PageResult<UserDto>>

{
    public int Page { get; }
    public int PageSize { get; }
    public string? Name { get; }
    public GetUsersQuery(int page, int pageSize, string? name)
    {
        Page = page;
        PageSize = pageSize;
        Name = name;
    }
}
