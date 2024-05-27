using System;
using PPI.Ordenes.Core.SharedKernel;

namespace PPI.Ordenes.Domain.Entities.AccountAggregate;
public class Account : BaseEntity, IAggregateRoot
{
    private bool _isDeleted;

    public Account(int idCuenta, string primerNombre, string segundoNombre, string tercerNombre, string apellido, decimal cuentaComitente, short estado, DateTime fechaCreacion)
    {
        IDCuenta = idCuenta;
        PrimerNombre = primerNombre;
        SegundoNombre = segundoNombre;
        TercerNombre = tercerNombre;
        Apellido = apellido;
        CuentaComitente = cuentaComitente;
        Estado = estado;
        FechaCreacion = fechaCreacion;
    }

    public Account()
    {
    }

    public int IDCuenta { get; }
    public string PrimerNombre { get; }
    public string SegundoNombre { get; }
    public string TercerNombre { get; }
    public string Apellido { get; }
    public decimal CuentaComitente { get; }
    public short Estado { get; private set; }
    public DateTime FechaCreacion { get; }
    public DateTime? FechaActualizacion { get; private set; }

    public void ChangeEstado(short newEstado)
    {
        if (Estado == newEstado)
            return;

        Estado = newEstado;
        FechaActualizacion = DateTime.Now;

        //AddDomainEvent(new AccountUpdatedEvent(IDCuenta, PrimerNombre, SegundoNombre, TercerNombre, Apellido, CuentaComitente, newEstado, FechaCreacion, FechaActualizacion.Value));
    }

    public void Delete()
    {
        if (_isDeleted) return;

        _isDeleted = true;
        //AddDomainEvent(new AccountDeletedEvent(IDCuenta, PrimerNombre, SegundoNombre, TercerNombre, Apellido, CuentaComitente, Estado, FechaCreacion, FechaActualizacion));
    }
}
