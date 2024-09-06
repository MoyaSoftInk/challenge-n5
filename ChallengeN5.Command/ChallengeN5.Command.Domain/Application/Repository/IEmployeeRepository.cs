namespace ChallengeN5.Command.Domain.Application.Repository;

using ChallengeN5.Command.Domain.Application.Model;
using ChallengeN5.Command.Domain.Architecture.Repository;

public interface IEmployeeRepository : IRepository<Employee, int>
{
}
