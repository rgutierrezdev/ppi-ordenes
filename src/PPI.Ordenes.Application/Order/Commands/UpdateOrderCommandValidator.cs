using FluentValidation;

namespace PPI.Ordenes.Application.Order.Commands;
public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(x => x.IdOrden)
            .NotEmpty();

        RuleFor(x => x.Estado)
            .NotEmpty()
            .IsInEnum()
            .WithMessage("Estado is not valid.");
    }
}
