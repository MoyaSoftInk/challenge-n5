namespace ChallengeN5.Command.API.Application.Command.PutPermission;

using ChallengeN5.Command.API.Architecture.Model;
using ChallengeN5.Command.Domain.Application.Model;
using ChallengeN5.Command.Domain.Application.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Put permission handler
/// </summary>
public class PutPermissionHandler : IRequestHandler<PutPermissionCommand, BaseResponse>
{
    private readonly IPermissionRepository _permissionRepository;

    /// <summary>
    /// Constructor
    /// </summary>
    public PutPermissionHandler(IPermissionRepository permissionRepository)
    {
        _permissionRepository = permissionRepository;
    }

    /// <summary>
    /// Handle the request
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="KeyNotFoundException"></exception>
    public async Task<BaseResponse> Handle(PutPermissionCommand request, CancellationToken cancellationToken)
    {
        Permission permission = await _permissionRepository.GetByIdAsync(request.PermissionId, cancellationToken) ??
            throw new KeyNotFoundException($"The permission with id:{request.PermissionId}, don't exist.");
        
        UpdatePermissionDetails(request, permission);

        await _permissionRepository.UpdateAsync(permission, cancellationToken);

        return new BaseResponse
        {
            HttpCode = 200,
            HttpMessage = "Permission was updated"
        };
    }

    /// <summary>
    /// update entity details
    /// </summary>
    /// <param name="request"></param>
    /// <param name="permission"></param>
    private static void UpdatePermissionDetails(PutPermissionCommand request, Permission permission)
    {
        permission.StartDate = request.StartDate;
        permission.EndDate = request.EndDate;
        permission.IsActive = request.IsActive;
    }
}
