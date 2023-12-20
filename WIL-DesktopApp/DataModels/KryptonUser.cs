namespace WIL_DesktopApp.DataModels;

/// <summary>
/// Table of users for login purposes
/// </summary>
public partial class KryptonUser
{
    /// <summary>
    /// Users username
    /// </summary>
    public string Username { get; set; } = null!;

    /// <summary>
    /// Hashed password
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Users info foreign key
    /// </summary>
    public int InfoId { get; set; }

    /// <summary>
    /// The type of user (0 employee, 1 Admin)
    /// </summary>
    public int UserType { get; set; }

    public virtual UserInfo Info { get; set; } = null!;
}
