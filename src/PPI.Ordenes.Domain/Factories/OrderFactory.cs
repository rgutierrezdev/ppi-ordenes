using System;
using Ardalis.Result;
using PPI.Ordenes.Domain.Entities.OrderAggregate;

namespace PPI.Ordenes.Domain.Factories;
public static class OrderFactory
{
    public static Result<Order> Create(        
        int idCuenta,
        string nombreActivo,
        int cantidad,
        decimal precio,
        char operacion,
        EOrderStatus estado,
        decimal montoTotal,
        DateTime fechaCreacion)
    {
        return Result<Order>.Success(new Order(idCuenta, nombreActivo, cantidad, precio, operacion, estado, montoTotal, fechaCreacion));
    }
}
