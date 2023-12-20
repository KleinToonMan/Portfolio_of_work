using System.Collections.Generic;

namespace WIL_DesktopApp.DataModels;

/// <summary>
/// Stores different values that attributes may need
/// </summary>
public partial class ValueType
{
    /// <summary>
    /// Primary key
    /// </summary>
    public int ValueTypeId { get; set; }

    /// <summary>
    /// Name of value type e.g. Length
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Description of value type
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Minimum value for numerical options
    /// </summary>
    public double? Min { get; set; }

    /// <summary>
    /// Maximum value for numerical options (i f any)
    /// </summary>
    public double? Max { get; set; }

    public virtual ICollection<ValueOptionList> ValueOptionLists { get; set; } = new List<ValueOptionList>();

    public virtual ICollection<ValueSelection> ValueSelections { get; set; } = new List<ValueSelection>();

    public virtual ICollection<ValueTypeList> ValueTypeLists { get; set; } = new List<ValueTypeList>();
}
