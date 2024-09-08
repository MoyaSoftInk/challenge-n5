namespace ChallengeN5.Query.Domain.Application.Service;

using ChallengeN5.Query.Domain.Application.Model;
using Nest;

public interface IElasticSearchService
{
    /// <summary>
    /// Search employee by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ISearchResponse<Permission>> SearchPermissionByEmployeeIdAsync(int id, CancellationToken cancellationToken);

    /// <summary>
    /// Index permission
    /// </summary>
    /// <param name="permission"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task IndexPermission(Permission permission, CancellationToken cancellationToken);
}
