namespace ChallengeN5.Command.Persistance.Application;

using ChallengeN5.Command.Domain.Application.Repository;
using ChallengeN5.Command.Domain.Architecture.Core;
using ChallengeN5.Command.Persistance.Application.Data;
using System.Threading.Tasks;

public class UnitOfWork : IUnitOfWork
{
    private readonly N5Context _context;

    public UnitOfWork(
        N5Context context, 
        IPermissionRepository permissionRepository)
    {
        _context = context;
        PermissionRepository = permissionRepository;
    }

    public IPermissionRepository PermissionRepository { get; }

    public async Task<int> CommitAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task<int> CommitAsync(CancellationToken cancellationToken)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }


    public void Dispose()
    {
        _context.Dispose();
    }
}
