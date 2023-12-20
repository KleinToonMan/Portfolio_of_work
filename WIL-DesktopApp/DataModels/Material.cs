using System.Collections.Generic;

namespace WIL_DesktopApp.DataModels;

/// <summary>
/// Material storage
/// </summary>
public partial class Material
{
    /// <summary>
    /// Primary key
    /// </summary>
    public int MaterialId { get; set; }

    /// <summary>
    /// Name of material
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Description of material
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Rand price of material
    /// </summary>
    public double Price { get; set; }

    public virtual ICollection<Attribute> Attributes { get; set; } = new List<Attribute>();
}
