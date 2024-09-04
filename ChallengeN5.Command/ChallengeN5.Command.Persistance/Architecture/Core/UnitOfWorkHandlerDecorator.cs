namespace ChallengeN5.Command.API.Architecture.Core;

using ChallengeN5.Command.Domain.Architecture.Core;
using MediatR;

/// <summary>
/// Unit of work handler decorator
/// </summary>
/// <typeparam name="TRequest"></typeparam>
public class UnitOfWorkHandlerDecorator<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IRequestHandler<TRequest, TResponse> _innerHandler;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="innerHandler"></param>
    /// <param name="unitOfWork"></param>
    public UnitOfWorkHandlerDecorator(IRequestHandler<TRequest, TResponse> innerHandler, IUnitOfWork unitOfWork)
    {
        _innerHandler = innerHandler;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Handle the request
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
    {
        var result = await _innerHandler.Handle(request, cancellationToken);
        await _unitOfWork.CommitAsync(); // Realiza el commit después de ejecutar el handler
        return result;
    }
}
