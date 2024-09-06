namespace ChallengeN5.Command.API.Application.Command.PostRequestPermision;

using ChallengeN5.Command.API.Architecture.Model;
using MediatR;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Post Request Permission Command
/// </summary>
public class PostRequestPermisionCommand : BaseRequest, IRequest<BaseResponse>
{
    /// <summary>
    /// Employee Id
    /// </summary>
    [Required]
    public int EmployeeId { get; set; }

    /// <summary>
    /// Permission Type Id
    /// </summary>
    [Required]
    public int PermissionTypeId { get; set; }

    /// <summary>
    /// Date when the permission starts
    /// </summary>
    [Required]
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Date when the permission ends
    /// </summary>
    [Required]
    public DateTime EndDate { get; set; }
}
