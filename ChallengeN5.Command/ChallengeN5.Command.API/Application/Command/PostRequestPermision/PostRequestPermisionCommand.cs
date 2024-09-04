namespace ChallengeN5.Command.API.Application.Command.PostRequestPermision;

using ChallengeN5.Command.API.Architecture.Model;
using MediatR;

/// <summary>
/// Post Request Permission Command
/// </summary>
public class PostRequestPermisionCommand : BaseRequest, IRequest<BaseResponse>
{
    /// <summary>
    /// Employee Id
    /// </summary>
    public int EmployeeId { get; set; }

    /// <summary>
    /// Permission Type Id
    /// </summary>
    public int PermissionTypeId { get; set; }

    /// <summary>
    /// Date when the permission starts
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Date when the permission ends
    /// </summary>
    public DateTime EndDate { get; set; }
}
