using System.Collections.Generic;

namespace WIL_DesktopApp.DataModels;

/// <summary>
/// Stores an option of a value that a user can select
/// </summary>
public partial class ValueOption
{
    /// <summary>
    /// Primary key
    /// </summary>
    public int ValueOptionId { get; set; }

    /// <summary>
    /// Name of value option e.g. Hatchback
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Actual value that this option represents e.g. 2 square meters
    /// </summary>
    public double Value { get; set; }

    public virtual ICollection<ValueOptionList> ValueOptionLists { get; set; } = new List<ValueOptionList>();
}
