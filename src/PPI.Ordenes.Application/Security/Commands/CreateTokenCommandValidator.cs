using FluentValidation;

namespace PPI.Ordenes.Application.Security.Commands;
public class CreateTokenCommandValidator : AbstractValidator<CreateTokenCommand>
{
    public CreateTokenCommandValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName es Requerido");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Password es Requerido");
    }
}
