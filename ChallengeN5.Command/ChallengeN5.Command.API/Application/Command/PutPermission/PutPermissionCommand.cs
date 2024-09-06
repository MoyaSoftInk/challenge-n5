namespace ChallengeN5.Command.API.Application.Command.PutPermission;

using ChallengeN5.Command.API.Architecture.Model;
using MediatR;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Put permission request
/// </summary>
public class PutPermissionCommand : BaseRequest, IRequest<BaseResponse>
{
    /// <summary>
    /// Permission id
    /// </summary>
    [Required]
    public int PermissionId { get; set; }

    /// <summary>
    /// Start date
    /// </summary>
    [Required]
    public DateTime StartDate { get; set; }

    /// <summary>
    /// End date
    /// </summary>
    [Required]
    public DateTime EndDate { get; set; }

    /// <summary>
    /// Is active
    /// </summary>
    [Required]
    public bool IsActive { get; set; }
}
