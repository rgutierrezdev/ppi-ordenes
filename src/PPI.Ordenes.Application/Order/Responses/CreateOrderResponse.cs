using PPI.Ordenes.Core.SharedKernel;

namespace PPI.Ordenes.Application.Order.Responses;
public class CreateOrderResponse(int IdOrden) : IResponse
{
    public int IdOrden { get; } = IdOrden;    
}
