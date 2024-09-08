namespace ChallengeN5.Query.API.Application.Query.GetPermissionByUserId;

using ChallengeN5.Query.API.Architecture.Model;
using MediatR;

/// <summary>
/// Get permission by user id query
/// </summary>
public class GetPermissionByUserIdQuery : BaseRequest, IRequest<GetPermissionByUserIdResponse>
{
    /// <summary>
    /// user id
    /// </summary>
    public int UserId { get; set; }
}
