namespace WIL_DesktopApp.DataModels;

/// <summary>
/// Stores value selected/entered by customers
/// </summary>
public partial class ValueSelection
{
    /// <summary>
    /// Primary key
    /// </summary>
    public int ValueSelectionId { get; set; }

    /// <summary>
    /// Foreign key for selected value
    /// </summary>
    public int ValueTypeId { get; set; }

    /// <summary>
    /// Foreign key for selected attribute
    /// </summary>
    public int AttributeSelectionId { get; set; }

    /// <summary>
    /// Value chosen/entered by customer
    /// </summary>
    public double Value { get; set; }

    public virtual AttributeSelection AttributeSelection { get; set; } = null!;

    public virtual ValueType ValueType { get; set; } = null!;
}
