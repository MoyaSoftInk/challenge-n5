namespace ChallengeN5.Command.API.Architecture;

using ChallengeN5.Command.API.Architecture.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

/// <summary>
/// Base controller for the application
/// </summary>
public class N5Controller : ControllerBase
{
    private readonly IMediator _mediator;
    private static string? NameMethodCall => MethodBase.GetCurrentMethod()!.DeclaringType!.FullName;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="mediator"></param>
    public N5Controller(IMediator mediator)
    {
        _mediator = mediator;
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
        if (!ModelState.IsValid || request is null) throw new Exception("!ModelState.IsValid");
       
        BaseResponse? response = await _mediator.Send(request, cancellationToken) as BaseResponse;
        
        return StatusCode(response!.HttpCode, response);
    }
}
