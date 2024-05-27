using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using FluentValidation;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using PPI.Ordenes.Application.Security.Commands;
using PPI.Ordenes.Application.Security.Responses;

namespace PPI.Ordenes.Application.Security.Handlers;
public class CreateTokenCommandHandler (IValidator<CreateTokenCommand> validator)
    : IRequestHandler<CreateTokenCommand, Result<CreateTokenCommandResponse>>
{
    public async Task<Result<CreateTokenCommandResponse>> Handle(CreateTokenCommand request, CancellationToken cancellationToken)
    {
        // Validating the request.
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return Result<CreateTokenCommandResponse>.Invalid(validationResult.AsErrors());

        var claims = new []
        {
            new Claim(ClaimTypes.Name, request.UserName),
            new Claim(ClaimTypes.Role, "User")
        };

        //temporal for demostration
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("&TAMG`*m{\"[~kLozHq~[M_X4bqZ64jgMb^SrUck[.B1HE6;;}+83QUmM}f1-a^R"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var secutiryToken = new JwtSecurityToken(                        
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(60),
                        signingCredentials: creds);

        var token = new JwtSecurityTokenHandler().WriteToken(secutiryToken);

        return Result<CreateTokenCommandResponse>.Success(new CreateTokenCommandResponse(token));
    }
}
