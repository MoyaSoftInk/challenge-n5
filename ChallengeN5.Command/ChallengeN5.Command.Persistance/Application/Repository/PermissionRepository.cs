namespace ChallengeN5.Command.Infrastructure.Application.Repository;

using ChallengeN5.Command.Domain.Application.Model;
using ChallengeN5.Command.Domain.Application.Repository;
using ChallengeN5.Command.Infrastructure.Application.Data;
using ChallengeN5.Command.Infrastructure.Architecture;
using Microsoft.Extensions.Logging;

public class PermissionRepository : Repository<Permission, int>, IPermissionRepository
{
    public PermissionRepository(N5Context context, ILogger<PermissionRepository> logger) : base(context, logger)
    {
    }
}
