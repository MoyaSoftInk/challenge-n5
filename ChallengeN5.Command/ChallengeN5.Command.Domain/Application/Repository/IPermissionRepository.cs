namespace ChallengeN5.Command.Domain.Application.Repository;

using ChallengeN5.Command.Domain.Application.Model;

public interface IPermissionRepository
{
    public Task CreateAsync(Permission entity, CancellationToken cancellationToken);

    public Task UpdateAsync(Permission entity, CancellationToken cancellationToken);

    /// <summary>
    /// Get permission by employee id and permission id
    /// </summary>
    /// <param name="employeeId">employee id</param>
    /// <param name="permissionId">permission id
    /// 
    /// </param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<Permission> GetAsync(int employeeId, int permissionId, CancellationToken cancellationToken);
}
