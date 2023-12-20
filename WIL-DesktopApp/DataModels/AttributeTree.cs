namespace WIL_DesktopApp.DataModels;

/// <summary>
/// Storing relational trees between attributes
/// </summary>
public partial class AttributeTree
{
    /// <summary>
    /// Primary key
    /// </summary>
    public int AttributeTreeId { get; set; }

    /// <summary>
    /// FK for parent attribute (if any)
    /// </summary>
    public int? ParentAttribute { get; set; }

    /// <summary>
    /// FK for child attribute (if any)
    /// </summary>
    public int? ChildAttribute { get; set; }

    public virtual Attribute? ChildAttributeNavigation { get; set; }

    public virtual Attribute? ParentAttributeNavigation { get; set; }
}
