using System;
using Ardalis.Result;
using PPI.Ordenes.Domain.Entities.AccountAggregate;

namespace PPI.Ordenes.Domain.Factories;
public static class AccountFactory
{
    public static Result<Account> Create(
        int idCuenta,
        string primerNombre,
        string segundoNombre,
        string tercerNombre,
        string apellido,
        decimal cuentaComitente,
        short estado,
        DateTime fechaCreacion)
    {
        return Result<Account>.Success(new Account(idCuenta, primerNombre, segundoNombre, tercerNombre, apellido, cuentaComitente, estado, fechaCreacion));
    }
}