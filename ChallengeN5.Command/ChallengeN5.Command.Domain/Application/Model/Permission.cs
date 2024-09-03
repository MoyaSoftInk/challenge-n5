namespace ChallengeN5.Command.Domain.Application.Model;

using ChallengeN5.Command.Domain.Architecture.SeedWork;

/// <summary>
/// Permission entity
/// </summary>
public class Permission : AuditableEntity<Permission, Guid>
{
    /// <summary>
    /// Employee Id
    /// </summary>
    public Guid EmployeeId { get; set; }

    /// <summary>
    /// Employee entity
    /// </summary>
    public virtual Employee Employee { get; set; } = null!;

    /// <summary>
    /// Permission Type Id
    /// </summary>
    public Guid PermissionTypeId { get; set; }

    /// <summary>
    /// Permission Type entity
    /// </summary>
    public virtual PermissionType PermissionType { get; set; } = null!;

    /// <summary>
    /// Date when the permission starts, if null it means the permission is inactive
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// Date when the permission ends
    /// </summary>
    public DateTime? EndDate { get; set; }
}
