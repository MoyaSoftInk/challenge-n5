namespace ChallengeN5.Query.Domain.Application.Model;

using ChallengeN5.Query.Domain.Architecture.SeedWork;

/// <summary>
/// Permission type entity
/// </summary>
public class PermissionType : AuditableEntity<PermissionType, int>
{
    /// <summary>
    /// Permission type name
    /// </summary>
    public string Name { get; set; } = null!;
}
