namespace WIL_DesktopApp.DataModels;

/// <summary>
/// Stores all settings for desktop and web app
/// </summary>
public partial class SystemSetting
{
    /// <summary>
    /// Primary key
    /// </summary>
    public int SettingId { get; set; }

    /// <summary>
    /// Name of setting
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Value of setting
    /// </summary>
    public string Value { get; set; } = null!;
}
