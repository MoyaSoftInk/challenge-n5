namespace ChallengeN5.Command.Domain.Architecture.Core;

/// <summary>
/// Unit of work interface
/// </summary>
public interface IUnitOfWork : IDisposable
{

    /// <summary>
    /// Commit the changes
    /// </summary>
    /// <returns></returns>
    Task<int> CommitAsync();


    /// <summary>
    /// Commit the changes
    /// </summary>
    /// <returns></returns>
    Task<int> CommitAsync(CancellationToken cancellationToken);
}
