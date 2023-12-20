using System.Collections.Generic;

namespace WIL_DesktopApp.DataModels;

/// <summary>
/// Stores individual attributes, including the product name
/// </summary>
public partial class Attribute
{
    /// <summary>
    /// ID of attribute
    /// </summary>
    public int AttributeId { get; set; }

    /// <summary>
    /// ID of associated material
    /// </summary>
    public int? MaterialId { get; set; }

    /// <summary>
    /// Name of attribute
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Description of attribute
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Modification of the cost price
    /// </summary>
    public double PriceModifier { get; set; }

    /// <summary>
    /// Whether to use a global cost modifier or not (e.g. length and width)
    /// </summary>
    public bool UseGlobalValue { get; set; }

    /// <summary>
    /// URL of associated image
    /// </summary>
    public string? ImgUrl { get; set; }

    public virtual ICollection<AttributeSelection> AttributeSelections { get; set; } = new List<AttributeSelection>();

    public virtual ICollection<AttributeTree> AttributeTreeChildAttributeNavigations { get; set; } = new List<AttributeTree>();

    public virtual ICollection<AttributeTree> AttributeTreeParentAttributeNavigations { get; set; } = new List<AttributeTree>();

    public virtual Material? Material { get; set; }

    public virtual ICollection<ValueTypeList> ValueTypeLists { get; set; } = new List<ValueTypeList>();
}
