using System.Collections.Generic;

namespace WIL_DesktopApp.DataModels;

/// <summary>
/// Stores personal info of employees
/// </summary>
public partial class UserInfo
{
    /// <summary>
    /// Primary key
    /// </summary>
    public int InfoId { get; set; }

    /// <summary>
    /// User first name
    /// </summary>
    public string FirstName { get; set; } = null!;

    /// <summary>
    /// User last name
    /// </summary>
    public string LastName { get; set; } = null!;

    /// <summary>
    /// User email
    /// </summary>
    public string Email { get; set; } = null!;

    public virtual ICollection<KryptonUser> KryptonUsers { get; set; } = new List<KryptonUser>();
}
