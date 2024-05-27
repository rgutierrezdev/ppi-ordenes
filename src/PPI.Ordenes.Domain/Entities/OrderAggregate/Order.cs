using System;
using PPI.Ordenes.Core.SharedKernel;
using PPI.Ordenes.Domain.Entities.AccountAggregate;

namespace PPI.Ordenes.Domain.Entities.OrderAggregate;
public class Order : BaseEntity, IAggregateRoot
{
    private bool _isDeleted;

    public Order(int idCuenta, string nombreActivo, int cantidad, decimal precio, char operacion, EOrderStatus estado, decimal montoTotal, DateTime fechaCreacion)
    {        
        IDCuenta = idCuenta;
        NombreActivo = nombreActivo;
        Cantidad = cantidad;
        Precio = precio;
        Operacion = operacion;
        Estado = estado;
        MontoTotal = montoTotal;
        FechaCreacion = fechaCreacion;

        //AddDomainEvent(new OrderCreatedEvent(Id, idCuenta, nombreActivo, cantidad, precio, operacion, estado, montoTotal, fechaCreacion));
    }

    public Order()
    {
    }

    public int IDOrden { get; set; }
    public int IDCuenta { get; }
    public string NombreActivo { get; }
    public int Cantidad { get; }
    public decimal Precio { get; }
    public char Operacion { get; }
    public EOrderStatus Estado { get; private set; }
    public decimal MontoTotal { get; }
    public DateTime FechaCreacion { get; }
    public DateTime? FechaActualizacion { get; private set; }

    public Account Account { get; set; } // Propiedad de navegación
    public void ChangeEstado(EOrderStatus newEstado)
    {
        if (Estado == newEstado)
            return;

        Estado = newEstado;
        FechaActualizacion = DateTime.Now;

        //AddDomainEvent(new OrderUpdatedEvent(Id, IDCuenta, NombreActivo, Cantidad, Precio, Operacion, newEstado, MontoTotal, FechaCreacion, FechaActualizacion.Value));
    }

    public void Delete()
    {
        if (_isDeleted) return;

        _isDeleted = true;
        //AddDomainEvent(new OrderDeletedEvent(Id, IDCuenta, NombreActivo, Cantidad, Precio, Operacion, Estado, MontoTotal, FechaCreacion, FechaActualizacion));
    }
}