using Ardalis.Result;
using MediatR;
using PPI.Ordenes.Application.Security.Responses;

namespace PPI.Ordenes.Application.Security.Commands;
public class CreateTokenCommand : IRequest<Result<CreateTokenCommandResponse>>
{
    public string UserName { get; set; }
    public string Password { get; set; }
}
