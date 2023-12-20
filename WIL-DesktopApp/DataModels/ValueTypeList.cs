namespace WIL_DesktopApp.DataModels;

/// <summary>
/// Stores value OPTIONS for an attribute
/// </summary>
public partial class ValueTypeList
{
    /// <summary>
    /// Primary key
    /// </summary>
    public int ValueListId { get; set; }

    /// <summary>
    /// Attribute the value is associated with
    /// </summary>
    public int AttributeId { get; set; }

    /// <summary>
    /// Value that is associated with the attribute
    /// </summary>
    public int ValueTypeId { get; set; }

    public virtual Attribute Attribute { get; set; } = null!;

    public virtual ValueType ValueType { get; set; } = null!;
}
