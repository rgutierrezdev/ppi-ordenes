using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PPI.Ordenes.Application.Order.Commands;
using PPI.Ordenes.Application.Order.Queries;
using PPI.Ordenes.Application.Order.Responses;
using PPI.Ordenes.Domain.Entities.OrderAggregate;
using PPI.Ordenes.PublicApi.Extensions;
using PPI.Ordenes.PublicApi.Models;

namespace PPI.Ordenes.PublicApi.Controllers.v1;

[Authorize]
[ApiController]
[ApiVersion("1.0")]
[Route("api/[controller]")]
public class OrdersController(IMediator mediator) : ControllerBase
{
    ////////////////////////
    // POST: /api/orders
    ////////////////////////

    /// <summary>
    /// Register a new order.
    /// </summary>
    /// <response code="200">Returns the Id of the new order.</response>
    /// <response code="400">Returns list of errors if the request is invalid.</response>
    /// <response code="500">When an unexpected internal error occurs on the server.</response>
    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ApiResponse<CreateOrderResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody][Required] CreateOrderCommand command) =>
        (await mediator.Send(command)).ToActionResult();

    ///////////////////////
    // PUT: /api/orders
    //////////////////////

    /// <summary>
    /// Updates an existing order.
    /// </summary>
    /// <response code="200">Returns the response with the success message.</response>
    /// <response code="400">Returns list of errors if the request is invalid.</response>
    /// <response code="404">When no order is found by the given Id.</response>
    /// <response code="500">When an unexpected internal error occurs on the server.</response>
    [HttpPut]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update([FromBody][Required] UpdateOrderCommand command) =>
        (await mediator.Send(command)).ToActionResult();

    //////////////////////////////
    // DELETE: /api/orders/{id}
    //////////////////////////////

    /// <summary>
    /// Delete the order by Id.
    /// </summary>
    /// <response code="200">Returns the response with the success message.</response>
    /// <response code="400">Returns list of errors if the request is invalid.</response>
    /// <response code="404">When no order is found by the given Id.</response>
    /// <response code="500">When an unexpected internal error occurs on the server.</response>
    [HttpDelete("{id:int}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete([Required] int id) =>
        (await mediator.Send(new DeleteOrderCommand(id))).ToActionResult();

    ///////////////////////////
    // GET: /api/orders/{id}
    ///////////////////////////

    /// <summary>
    /// Get the order by Id.
    /// </summary>
    /// <response code="200">Returns the client.</response>
    /// <response code="400">Returns list of errors if the request is invalid.</response>
    /// <response code="404">When no order is found by the given Id.</response>
    /// <response code="500">When an unexpected internal error occurs on the server.</response>
    [HttpGet("{id:int}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ApiResponse<Order>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById([Required] int id) =>
        (await mediator.Send(new GetOrderByIdQuery(id))).ToActionResult();

    //////////////////////
    // GET: /api/orders
    //////////////////////

    /// <summary>
    /// Gets a list of all orders.
    /// </summary>
    /// <response code="200">Returns the list of clients.</response>
    /// <response code="500">When an unexpected internal error occurs on the server.</response>
    [HttpGet]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<Order>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll() =>
        (await mediator.Send(new GetAllOrderQuery())).ToActionResult();
}
