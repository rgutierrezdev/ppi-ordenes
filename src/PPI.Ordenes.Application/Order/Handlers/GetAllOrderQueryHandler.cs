using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using PPI.Ordenes.Application.Order.Queries;
using PPI.Ordenes.Domain.Entities.OrderAggregate;
using OrderModel = PPI.Ordenes.Domain.Entities.OrderAggregate.Order;

namespace PPI.Ordenes.Application.Order.Handlers;
public class GetAllOrderQueryHandler(
    IOrderWriteOnlyRepository orderWriteOnlyRepository)
    : IRequestHandler<GetAllOrderQuery, Result<IEnumerable<OrderModel>>>
{
    public async Task<Result<IEnumerable<OrderModel>>> Handle(GetAllOrderQuery request, CancellationToken cancellationToken)
    {
        var orders = await orderWriteOnlyRepository.GetAllAsync();
        return Result<IEnumerable<OrderModel>>.Success(orders);
    }
}
