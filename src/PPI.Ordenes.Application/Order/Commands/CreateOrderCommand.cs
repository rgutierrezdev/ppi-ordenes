using System.ComponentModel.DataAnnotations;
using Ardalis.Result;
using MediatR;
using PPI.Ordenes.Application.Order.Responses;

namespace PPI.Ordenes.Application.Order.Commands;
public class CreateOrderCommand : IRequest<Result<CreateOrderResponse>>
{
    [Required]
    public int IdCuenta { get; set; }

    [Required]
    [MaxLength(100)]
    [DataType(DataType.Text)]
    public string NombreActivo { get; set; }

    [Required]
    public int Cantidad { get; set; }

    [Required]
    public char Operacion { get; set; }   
}