using MediatR;
using Similar_products.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Similar_products.Application.Requests.Queries
{
    public class GetProductsAllQuery : IRequest<IEnumerable<ProductDto>>
    {
    }
}
