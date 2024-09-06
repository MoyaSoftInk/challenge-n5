namespace ChallengeN5.Command.API.Application.Command.PostRequestPermision;

using ChallengeN5.Command.API.Architecture.Model;
using ChallengeN5.Command.Domain.Application.Model;
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
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IPermissionTypeRepository _permissionTypeRepository;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="permissionRepository"></param>
    /// <param name="employeeRepository"></param>
    /// <param name="permissionTypeRepository"></param>
    public PostRequestPermisionHandler(
        IPermissionRepository permissionRepository,
        IEmployeeRepository employeeRepository,
        IPermissionTypeRepository permissionTypeRepository)
    {
        _permissionRepository = permissionRepository;
        _employeeRepository = employeeRepository;
        _permissionTypeRepository = permissionTypeRepository;
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
        Employee employee = new();
        PermissionType permissionType = new();

        await ValidateAndRetrieveEntities(request, employee, permissionType, cancellationToken);

        await _permissionRepository.CreateAsync(new Permission
        {
            Employee = employee,
            PermissionType = permissionType,
            StartDate = request.StartDate,
            EndDate = request.EndDate
        }, cancellationToken);

        return new BaseResponse
        {
            HttpCode = 201,
            HttpMessage = "Permission was requested"
        };
    }

    /// <summary>
    /// Validate and retrieve entities
    /// </summary>
    /// <param name="request"></param>
    /// <param name="employee"></param>
    /// <param name="permissionType"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="KeyNotFoundException"></exception>
    /// <exception cref="Exception"></exception>
    private async Task ValidateAndRetrieveEntities(PostRequestPermisionCommand request, Employee employee, PermissionType permissionType, CancellationToken cancellationToken)
    {
        employee = await _employeeRepository.GetByIdAsync(request.EmployeeId, cancellationToken) ??
                    throw new KeyNotFoundException($"The employee with Id: {request.EmployeeId}, don't exist");
        permissionType = await _permissionTypeRepository.GetByIdAsync(request.PermissionTypeId, cancellationToken) ??
            throw new KeyNotFoundException($"The permission type with Id: {request.PermissionTypeId}, don't exist");
        if ((await _permissionRepository.GetFilteredAsync(p => p.Employee.Id == employee.Id && p.PermissionType.Id == permissionType.Id, c => c.Id, true, cancellationToken)).Any())
        {
            throw new Exception($"The employee with Id: {request.EmployeeId}, already has a permission of type: {permissionType.Name}");
        }
    }
}
