namespace WIL_DesktopApp.DataModels;

/// <summary>
/// Stores the list of options for the customer to select 
/// </summary>
public partial class ValueOptionList
{
    /// <summary>
    /// Primary key
    /// </summary>
    public int ValueOptionListId { get; set; }

    public int ValueOptionId { get; set; }

    public int ValueTypeId { get; set; }

    public virtual ValueOption ValueOption { get; set; } = null!;

    public virtual ValueType ValueType { get; set; } = null!;
}
