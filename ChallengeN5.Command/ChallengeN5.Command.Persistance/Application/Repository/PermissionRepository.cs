namespace ChallengeN5.Command.Persistance.Application.Repository;

using ChallengeN5.Command.Domain.Application.Model;
using ChallengeN5.Command.Domain.Application.Repository;
using ChallengeN5.Command.Persistance.Application.Data;
using ChallengeN5.Command.Persistance.Architecture;

public class PermissionRepository : CreateRepository<Permission, int>, IPermissionRepository
{
    public PermissionRepository(N5Context context) : base(context)
    {
    }
}
