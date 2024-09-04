namespace ChallengeN5.Command.Domain.Application.Model;

using ChallengeN5.Command.Domain.Architecture.SeedWork;

/// <summary>
/// Permission entity
/// </summary>
public class Permission : AuditableEntity<Permission, int>
{
    /// <summary>
    /// Employee Id
    /// </summary>
    public int EmployeeId { get; set; }

    /// <summary>
    /// Employee entity
    /// </summary>
    public virtual Employee Employee { get; set; } = null!;

    /// <summary>
    /// Permission Type Id
    /// </summary>
    public int PermissionTypeId { get; set; }

    /// <summary>
    /// Permission Type entity
    /// </summary>
    public virtual PermissionType PermissionType { get; set; } = null!;

    /// <summary>
    /// Date when the permission starts
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Date when the permission ends
    /// </summary>
    public DateTime EndDate { get; set; }

    /// <summary>
    /// Indicates if the permission is active
    /// </summary>
    public bool IsActive { get; set; } = false;
}
