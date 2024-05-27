using FluentValidation;

namespace PPI.Ordenes.Application.Order.Commands;
public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(command => command.IdCuenta)
            .NotEmpty();

        RuleFor(command => command.NombreActivo)
            .NotEmpty()
            .MaximumLength(32)
            .WithMessage("NombreActivo debe tener menos de 32 caracteres");

        RuleFor(command => command.Cantidad)
            .NotEmpty()            
            .GreaterThan(0)
            .WithMessage("Cantidad debe ser mayor a 0");

        RuleFor(command => command.Operacion)
            .NotEmpty()
            .Must(value => value == 'C' || value == 'V')
            .WithMessage("Operacion debe ser 'C' o 'V'");
    }
}
