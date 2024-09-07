namespace ChallengeN5.Command.Persistance.Application.Repository;

using ChallengeN5.Command.Domain.Application.Model;
using ChallengeN5.Command.Domain.Application.Repository;
using ChallengeN5.Command.Persistance.Application.Data;
using ChallengeN5.Command.Persistance.Architecture;
using Microsoft.Extensions.Logging;

public class EmployeeRepository : Repository<Employee, int>, IEmployeeRepository
{
    public EmployeeRepository(N5Context context, ILogger<EmployeeRepository> logger) : base(context, logger)
    {
    }
}
