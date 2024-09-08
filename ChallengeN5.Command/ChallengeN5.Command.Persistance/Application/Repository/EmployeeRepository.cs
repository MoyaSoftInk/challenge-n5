namespace ChallengeN5.Command.Infrastructure.Application.Repository;

using ChallengeN5.Command.Domain.Application.Model;
using ChallengeN5.Command.Domain.Application.Repository;
using ChallengeN5.Command.Infrastructure.Application.Data;
using ChallengeN5.Command.Infrastructure.Architecture;
using Microsoft.Extensions.Logging;

public class EmployeeRepository : Repository<Employee, int>, IEmployeeRepository
{
    public EmployeeRepository(N5Context context, ILogger<EmployeeRepository> logger) : base(context, logger)
    {
    }
}
