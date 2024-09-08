namespace ChallengeN5.Query.API.Application.Query.GetPermissionByUserId;

using ChallengeN5.Query.API.Application.ViewModel;
using ChallengeN5.Query.Domain.Application.Model;
using ChallengeN5.Query.Domain.Application.Service;
using MediatR;
using Nest;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// get permission by user id handler
/// </summary>
public class GetPermissionByUserIdHandler : IRequestHandler<GetPermissionByUserIdQuery, GetPermissionByUserIdResponse>
{
    private readonly IElasticSearchService _elasticSearchService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="elasticSearchService"></param>
    public GetPermissionByUserIdHandler(IElasticSearchService elasticSearchService)
    {
        _elasticSearchService = elasticSearchService;
    }

    public async Task<GetPermissionByUserIdResponse> Handle(GetPermissionByUserIdQuery request, CancellationToken cancellationToken)
    {
        ISearchResponse<Permission> response = 
            await _elasticSearchService.SearchPermissionByEmployeeIdAsync(request.UserId, cancellationToken);
               
        if(!response.IsValid)
        {
            throw new Exception("Error while searching permission");
        }

        return new GetPermissionByUserIdResponse
        {
            Permissions = ProcessSearchResponse(response)
        };

    }

    private static IList<PermissionViewModel> ProcessSearchResponse(ISearchResponse<Permission> searchResponse)
    {
        IList<PermissionViewModel> permissionViewModel = new List<PermissionViewModel>();

        foreach (var hit in searchResponse.Hits)
        {
            // Acceder al objeto Permission
            Permission permission = hit.Source;

            permissionViewModel.Add(new PermissionViewModel
            {
                EmployeeId = permission.EmployeeId,
                PermissionTypeId = permission.PermissionTypeId,
                StartDate = permission.StartDate,
                EndDate = permission.EndDate,
                IsActive = permission.IsActive

            });
        }

        return permissionViewModel;
    }
}
