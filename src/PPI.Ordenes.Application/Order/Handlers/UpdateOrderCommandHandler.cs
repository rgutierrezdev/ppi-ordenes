using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using FluentValidation;
using MediatR;
using PPI.Ordenes.Application.Order.Commands;
using PPI.Ordenes.Core.SharedKernel;
using PPI.Ordenes.Domain.Entities.OrderAggregate;

namespace PPI.Ordenes.Application.Order.Handlers;
public class UpdateOrderCommandHandler(IValidator<UpdateOrderCommand> validator,
    IOrderWriteOnlyRepository orderWriteOnlyRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateOrderCommand, Result>
{
    public async Task<Result> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        // Validating the request.
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)        
            return Result.Invalid(validationResult.AsErrors());

        var orden = await orderWriteOnlyRepository.GetByIdOrdenAsync(request.IdOrden);
        if (orden == null)
            return Result.Error("La Orden no existe.");

        orden.ChangeEstado(request.Estado);

        orderWriteOnlyRepository.Update(orden);

        await unitOfWork.SaveChangesAsync();

        return Result.SuccessWithMessage("Orden actualizada correctamente.");
    }
}
