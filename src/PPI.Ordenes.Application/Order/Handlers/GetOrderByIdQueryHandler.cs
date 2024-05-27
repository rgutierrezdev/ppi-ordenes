using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using PPI.Ordenes.Application.Order.Queries;
using PPI.Ordenes.Domain.Entities.OrderAggregate;
using OrderModel = PPI.Ordenes.Domain.Entities.OrderAggregate.Order;

namespace PPI.Ordenes.Application.Order.Handlers;
public class GetOrderByIdQueryHandler(IOrderWriteOnlyRepository orderWriteOnlyRepository) : IRequestHandler<GetOrderByIdQuery, Result<OrderModel>>
{
    public async Task<Result<OrderModel>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await orderWriteOnlyRepository.GetByIdOrdenAsync(request.Id);
        if (order == null)
            return Result<OrderModel>.Error("La Orden no existe.");

        return order;        
    }
}
