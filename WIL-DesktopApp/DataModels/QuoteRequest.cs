using System.Collections.Generic;

namespace WIL_DesktopApp.DataModels;

/// <summary>
/// Request for a quote from a customer
/// </summary>
public partial class QuoteRequest
{
    /// <summary>
    /// Primary key
    /// </summary>
    public int QuoteRequestId { get; set; }

    /// <summary>
    /// Email of customer requesting quote
    /// </summary>
    public string Email { get; set; } = null!;

    /// <summary>
    /// Any notes regarding the quote
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Url of the files the customer uploaded
    /// </summary>
    public string? ImgUrl { get; set; }



    public virtual ICollection<RequestItem> RequestItems { get; set; } = new List<RequestItem>();
}
