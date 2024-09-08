namespace ChallengeN5.Query.API.Application.ViewModel;

/// <summary>
/// Permission viewmodel
/// </summary>
public class PermissionViewModel
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

    /// <summary>
    /// Indicates if the permission is active
    /// </summary>
    public bool IsActive { get; set; } = false;
}
