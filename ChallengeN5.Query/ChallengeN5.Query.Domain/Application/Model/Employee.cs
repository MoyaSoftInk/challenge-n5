namespace ChallengeN5.Query.Domain.Application.Model;

using ChallengeN5.Query.Domain.Architecture.SeedWork;

/// <summary>
/// Employee entity
/// </summary>
public class Employee : AuditableEntity<Employee, int>
{
    /// <summary>
    /// First name of the employee
    /// </summary>
    public string FirstName { get; set; } = null!;

    /// <summary>
    /// Last name of the employee
    /// </summary>
    public string LastName { get; set; } = null!;

    /// <summary>
    /// The position of the employee
    /// </summary>
    public string Position { get; set; } = null!;
}
