namespace ChallengeN5.Query.API.Application.Query.GetPermissionByUserId;

using ChallengeN5.Query.API.Architecture.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Get permission by user id query
/// </summary>
public class GetPermissionByUserIdQuery : BaseRequest, IRequest<GetPermissionByUserIdResponse>
{
    /// <summary>
    /// user id
    /// </summary>
    [Required]
    [FromRoute]
    public int UserId { get; set; }
}
