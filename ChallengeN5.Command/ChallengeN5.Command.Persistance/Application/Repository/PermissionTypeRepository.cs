namespace ChallengeN5.Command.Persistance.Application.Repository;

using ChallengeN5.Command.Domain.Application.Model;
using ChallengeN5.Command.Domain.Application.Repository;
using ChallengeN5.Command.Persistance.Application.Data;
using ChallengeN5.Command.Persistance.Architecture;
using Microsoft.Extensions.Logging;

public class PermissionTypeRepository : Repository<PermissionType, int>, IPermissionTypeRepository
{
    public PermissionTypeRepository(N5Context context, ILogger<PermissionTypeRepository> logger) : base(context, logger)
    {
    }
}
