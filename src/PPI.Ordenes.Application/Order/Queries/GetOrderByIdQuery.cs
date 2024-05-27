using Ardalis.Result;
using MediatR;
using OrderModel = PPI.Ordenes.Domain.Entities.OrderAggregate.Order;

namespace PPI.Ordenes.Application.Order.Queries;
public class GetOrderByIdQuery(int idOrden) : IRequest<Result<OrderModel>>
{
    public int Id { get; } = idOrden;
}