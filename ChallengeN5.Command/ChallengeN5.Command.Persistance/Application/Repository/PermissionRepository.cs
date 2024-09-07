namespace ChallengeN5.Command.Persistance.Application.Repository;

using ChallengeN5.Command.Domain.Application.Model;
using ChallengeN5.Command.Domain.Application.Repository;
using ChallengeN5.Command.Persistance.Application.Data;
using ChallengeN5.Command.Persistance.Architecture;
using Microsoft.Extensions.Logging;

public class PermissionRepository : Repository<Permission, int>, IPermissionRepository
{
    public PermissionRepository(N5Context context, ILogger<PermissionRepository> logger) : base(context, logger)
    {
    }
}
