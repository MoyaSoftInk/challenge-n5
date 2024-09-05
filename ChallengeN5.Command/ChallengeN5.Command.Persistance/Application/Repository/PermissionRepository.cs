namespace ChallengeN5.Command.Persistance.Application.Repository;

using ChallengeN5.Command.Domain.Application.Model;
using ChallengeN5.Command.Domain.Application.Repository;
using ChallengeN5.Command.Persistance.Application.Data;
using ChallengeN5.Command.Persistance.Architecture;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public class PermissionRepository : CreateRepository<Permission, int>, IPermissionRepository
{
    public PermissionRepository(N5Context context) : base(context)
    {
    }

    public IEnumerable<Permission> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Permission>> GetAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Permission? GetById(int key)
    {
        throw new NotImplementedException();
    }

    public Task<Permission?> GetByIdAsync(int key, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
