using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PPI.Ordenes.Application.Security.Commands;
using PPI.Ordenes.Application.Security.Responses;
using PPI.Ordenes.PublicApi.Extensions;
using PPI.Ordenes.PublicApi.Models;

namespace PPI.Ordenes.PublicApi.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/[controller]")]
public class SecurityController(IMediator mediator) : ControllerBase
{
    ////////////////////////
    // POST: /api/orders
    ////////////////////////

    /// <summary>
    /// Register a new order.
    /// </summary>
    /// <response code="200">Returns token JWT.</response>
    /// <response code="400">Returns list of errors if the request is invalid.</response>
    /// <response code="500">When an unexpected internal error occurs on the server.</response>
    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ApiResponse<CreateTokenCommandResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody][Required] CreateTokenCommand command) =>
        (await mediator.Send(command)).ToActionResult();
}
