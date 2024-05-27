using PPI.Ordenes.Core.SharedKernel;

namespace PPI.Ordenes.Application.Security.Responses;
public class CreateTokenCommandResponse(string token) : IResponse
{
    public string Token { get; } = token;
}
