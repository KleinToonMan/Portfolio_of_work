using System.Collections.Generic;

namespace WIL_DesktopApp.DataModels;

/// <summary>
/// Storing each individual item (comprised of attributes) req.
/// </summary>
public partial class RequestItem
{
    /// <summary>
    /// Primary key
    /// </summary>
    public int RequestItemId { get; set; }

    /// <summary>
    /// Foreign key referencing quote request
    /// </summary>
    public int QuoteRequestId { get; set; }

    /// <summary>
    /// How many of this item has been requested
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// The estimate given to the customer at the time of their request
    /// </summary>
    public double EstimateGiven { get; set; }

    public virtual ICollection<AttributeSelection> AttributeSelections { get; set; } = new List<AttributeSelection>();

    public virtual QuoteRequest QuoteRequest { get; set; } = null!;
}
