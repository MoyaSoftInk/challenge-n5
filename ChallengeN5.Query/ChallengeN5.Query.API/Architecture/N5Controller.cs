namespace ChallengeN5.Query.API.Architecture;

using ChallengeN5.Query.API.Architecture.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

/// <summary>
/// Base controller for the application
/// </summary>
public class N5Controller : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger _logger;
    private static string? NameMethodCall => MethodBase.GetCurrentMethod()!.DeclaringType!.FullName;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="logger"></param>
    public N5Controller(IMediator mediator, ILogger logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Run the handler for the request
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    protected async Task<ActionResult> RunHandler(BaseRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{NameMethodCall} - {request.GetType().Name} - {request}");
        if (!ModelState.IsValid || request is null) throw new Exception("!ModelState.IsValid");

        BaseResponse? response = await _mediator.Send(request, cancellationToken) as BaseResponse;
        _logger.LogInformation($"{NameMethodCall} - {request.GetType().Name} - {response}");
        return StatusCode(response!.HttpCode, response);
    }
}
