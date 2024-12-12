using MediatR;
using Similar_products.Application.Dtos;
using Similar_products.Domain.Entities;

namespace Similar_products.Application.Requests.Queries;

public record GetUsersAllQuery : IRequest<IEnumerable<User>>
{
    public string? UserName { get; set; }
    public GetUsersAllQuery(string? userName)
    {
        UserName = userName;
    }
}