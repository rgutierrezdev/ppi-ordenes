using System.ComponentModel;

namespace PPI.Ordenes.Domain.Entities.OrderAggregate;
public enum EOrderStatus
{
    [Description("En proceso")]
    EnProceso = 0,

    [Description("Ejecutada")]
    Ejecutada = 1,

    [Description("Cancelada")]
    Cancelada = 2
}
