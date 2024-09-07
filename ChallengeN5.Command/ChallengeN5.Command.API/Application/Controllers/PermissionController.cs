namespace ChallengeN5.Command.API.Application.Controllers;

using ChallengeN5.Command.API.Application.Command.PostRequestPermision;
using ChallengeN5.Command.API.Application.Command.PutPermission;
using ChallengeN5.Command.API.Architecture;
using ChallengeN5.Command.API.Architecture.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

/// <summary>
/// Permission Controller
/// </summary>
[Route("[controller]")]
public class PermissionController : N5Controller
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="logger"></param>
    public PermissionController(IMediator mediator, ILogger<PermissionController> logger) : base(mediator, logger)
    {
    }

    /// <summary>
    /// Request a permission
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /permission/request
    ///
    /// </remarks>
    /// <param name="command">body</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <response code="200">Success request</response>
    /// <response code="400">The request can't be resolve.</response>
    /// <response code="404">The request not found.</response>
    /// <response code="500">Internal server error</response>
    /// <returns>Create the resource</returns>
    [HttpPost("request")]
    [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorBaseResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorBaseResponse), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ErrorBaseResponse), (int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult> RequestPermission([FromBody] PostRequestPermisionCommand command, CancellationToken cancellationToken)
    {
        return await RunHandler(command, cancellationToken);
    }

    /// <summary>
    /// Update a permission
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     PUT /permission
    ///
    /// </remarks>
    /// <param name="command">body</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <response code="200">Success request</response>
    /// <response code="400">The request can't be resolve.</response>
    /// <response code="404">The request not found.</response>
    /// <response code="500">Internal server error</response>
    /// <returns>Create the resource</returns>
    [HttpPut]
    [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorBaseResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorBaseResponse), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ErrorBaseResponse), (int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult> UpdatePermission([FromBody] PutPermissionCommand command, CancellationToken cancellationToken)
    {
        return await RunHandler(command, cancellationToken);
    }
}
