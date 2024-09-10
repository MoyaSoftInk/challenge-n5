namespace ChallengeN5.Query.API.Application.Controllers;

using ChallengeN5.Query.API.Application.Query.GetPermissionByUserId;
using ChallengeN5.Query.API.Architecture;
using ChallengeN5.Query.API.Architecture.Model;
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
    /// Get permission
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /permission
    ///
    /// </remarks>
    /// <param name="query">body</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <response code="200">Success request</response>
    /// <response code="400">The request can't be resolve.</response>
    /// <response code="404">The request not found.</response>
    /// <response code="500">Internal server error</response>
    /// <returns>Create the resource</returns>
    [HttpGet("{UserId}")]
    [ProducesResponseType(typeof(GetPermissionByUserIdResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorBaseResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorBaseResponse), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ErrorBaseResponse), (int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult> RequestPermission([FromRoute] GetPermissionByUserIdQuery query, CancellationToken cancellationToken)
    {
        return await RunHandler(query, cancellationToken);
    }

}
