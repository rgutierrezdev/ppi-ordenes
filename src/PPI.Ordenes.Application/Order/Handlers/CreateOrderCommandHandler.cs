using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using FluentValidation;
using MediatR;
using PPI.Ordenes.Application.Order.Commands;
using PPI.Ordenes.Application.Order.Responses;
using PPI.Ordenes.Core.SharedKernel;
using PPI.Ordenes.Domain.Entities.AccountAggregate;
using PPI.Ordenes.Domain.Entities.OrderAggregate;
using PPI.Ordenes.Domain.Factories;
using PPI.Ordenes.Domain.ValueObjects;
using PPI.Ordenes.Query.Data.Repositories.Abstractions;

namespace PPI.Ordenes.Application.Order.Handlers;
public class CreateOrderCommandHandler(
    IValidator<CreateOrderCommand> validator,
    IOrderWriteOnlyRepository repository,
    IAssetReadOnlyRepository assetReadOnlyRepository,
    IAccountReadOnlyRepository accountReadOnlyRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateOrderCommand, Result<CreateOrderResponse>>
{
    public async Task<Result<CreateOrderResponse>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        // Validating the request.
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)                    
            return Result<CreateOrderResponse>.Invalid(validationResult.AsErrors());

        // Get and Checking if the asset exists.
        var asset = await assetReadOnlyRepository.GetByTickerAsync(request.NombreActivo);
        if (asset == null)
            return Result<CreateOrderResponse>.Error("El activo no existe");

        // Get and Checking if the account exists.
        var account = await accountReadOnlyRepository.GetByAccountIdAsync(request.IdCuenta);
        if (account == null)
            return Result<CreateOrderResponse>.Error("La cuenta no existe");

        // Creating an instance of the MontoTotal value object.
        var montoTotal = MontoTotal.Create(asset.PrecioUnitario, request.Cantidad, asset.TipoActivo);
        if (!montoTotal.IsSuccess)
            return Result<CreateOrderResponse>.Error("Error al generar el Monto Total de la orden");

        // Creating an instance of the order entity.
        var order = OrderFactory.Create(                
                request.IdCuenta,
                request.NombreActivo,
                request.Cantidad,
                asset.PrecioUnitario,
                request.Operacion,
                EOrderStatus.EnProceso,
                montoTotal.Value.Valor,
                DateTime.Now).Value;

        // Adding the entity to the repository.
        repository.Add(order);

        // Saving changes to the database.
        await unitOfWork.SaveChangesAsync();

        // Returning the ID and success message.
        return Result<CreateOrderResponse>.Success(
            new CreateOrderResponse(order.IDOrden), "Orden creada exitosamente!");
    }
}
