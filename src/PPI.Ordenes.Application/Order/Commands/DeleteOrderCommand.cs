using System.ComponentModel.DataAnnotations;
using Ardalis.Result;
using MediatR;

namespace PPI.Ordenes.Application.Order.Commands;
public class DeleteOrderCommand(int idOrden) : IRequest<Result>
{
    [Required]
    public int IdOrden { get; } = idOrden;
}

