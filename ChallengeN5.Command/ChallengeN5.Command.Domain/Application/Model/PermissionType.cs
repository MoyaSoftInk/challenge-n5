namespace ChallengeN5.Command.Domain.Application.Model;

using ChallengeN5.Command.Domain.Architecture.SeedWork;

/// <summary>
/// Permission type entity
/// </summary>
public class PermissionType : AuditableEntity<PermissionType, Guid>
{
    /// <summary>
    /// Permission type name
    /// </summary>
    public string Name { get; set; } = null!;
}
