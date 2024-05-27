using System.ComponentModel.DataAnnotations;
using Ardalis.Result;
using MediatR;
using PPI.Ordenes.Domain.Entities.OrderAggregate;

namespace PPI.Ordenes.Application.Order.Commands;
public class UpdateOrderCommand : IRequest<Result>
{
    [Required]
    public int IdOrden { get; set; }

    [Required]        
    public EOrderStatus Estado { get; set; }
}
