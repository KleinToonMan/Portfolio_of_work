using System.Collections.Generic;

namespace WIL_DesktopApp.DataModels;

/// <summary>
/// Stores the customer selected attribute
/// </summary>
public partial class AttributeSelection
{
    /// <summary>
    /// Primary key
    /// </summary>
    public int AttributeSelectionId { get; set; }

    /// <summary>
    /// Foreign key associated with selected attribute
    /// </summary>
    public int AttributeId { get; set; }

    /// <summary>
    /// Foreign key for product item this attribute is associated with
    /// </summary>
    public int RequestItemId { get; set; }

    public virtual Attribute Attribute { get; set; } = null!;

    public virtual RequestItem RequestItem { get; set; } = null!;

    public virtual ICollection<ValueSelection> ValueSelections { get; set; } = new List<ValueSelection>();
}
