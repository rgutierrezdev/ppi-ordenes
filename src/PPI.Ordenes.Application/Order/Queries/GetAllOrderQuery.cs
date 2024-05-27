using System.Collections.Generic;
using Ardalis.Result;
using MediatR;
using OrderModel = PPI.Ordenes.Domain.Entities.OrderAggregate.Order;
namespace PPI.Ordenes.Application.Order.Queries;
public class GetAllOrderQuery : IRequest<Result<IEnumerable<OrderModel>>>;

