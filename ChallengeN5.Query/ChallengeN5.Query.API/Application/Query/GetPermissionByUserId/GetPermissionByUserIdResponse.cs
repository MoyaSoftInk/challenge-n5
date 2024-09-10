namespace ChallengeN5.Query.API.Application.Query.GetPermissionByUserId;

using ChallengeN5.Query.API.Application.ViewModel;
using ChallengeN5.Query.API.Architecture.Model;

/// <summary>
/// Permission response
/// </summary>
public class GetPermissionByUserIdResponse : BaseResponse
{
    /// <summary>
    /// Permission
    /// </summary>
    public IList<PermissionViewModel> Permissions { get; set; } = [];
}
