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
public class DeleteOrderCommandHandler(
    IValidator<DeleteOrderCommand> validator,
    IOrderWriteOnlyRepository orderWriteOnlyRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteOrderCommand, Result>
{
    public async Task<Result> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)        
            return Result.Invalid(validationResult.AsErrors());
        
        var order = await orderWriteOnlyRepository.GetByIdOrdenAsync(request.IdOrden);
        if (order == null)
            return Result.Error("La Orden no existe.");

        order.Delete();

        orderWriteOnlyRepository.Remove(order);

        await unitOfWork.SaveChangesAsync();
        return Result.SuccessWithMessage("Orden eliminada correctamente.");
    }
}

