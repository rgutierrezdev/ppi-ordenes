using FluentValidation;

namespace PPI.Ordenes.Application.Order.Commands;
public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
{
    public DeleteOrderCommandValidator()
    {
        RuleFor(command => command.IdOrden)
            .NotEmpty()
            .WithMessage("El Id de la Orden es requerido.");
    }
}
