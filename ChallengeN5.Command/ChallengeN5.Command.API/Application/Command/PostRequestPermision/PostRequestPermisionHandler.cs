namespace ChallengeN5.Command.API.Application.Command.PostRequestPermision;

using ChallengeN5.Command.API.Architecture.Model;
using ChallengeN5.Command.Domain.Application.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Request permission handler
/// </summary>
public class PostRequestPermisionHandler : IRequestHandler<PostRequestPermisionCommand, BaseResponse>
{
    private readonly IPermissionRepository _permissionRepository;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="permissionRepository"></param>
    public PostRequestPermisionHandler(IPermissionRepository permissionRepository)
    {
        _permissionRepository = permissionRepository;
    }

    /// <summary>
    /// Handle the request
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<BaseResponse> Handle(PostRequestPermisionCommand request, CancellationToken cancellationToken)
    {
        await _permissionRepository.CreateAsync(new Domain.Application.Model.Permission
        {
            EmployeeId = request.EmployeeId,
            PermissionTypeId = request.PermissionTypeId,
            StartDate = request.StartDate,
            EndDate = request.EndDate
        }, cancellationToken);

        return new BaseResponse
        {
            HttpCode = 201,
            HttpMessage = "Permission created"
        };
    }
}
