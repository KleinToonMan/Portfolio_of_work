namespace WIL_DesktopApp.Models
{
    /*
     * <summary>
     * Model of a quote made up either from a request or from scratch
     * </summary>
     */
    public class Quote
    {
        public string CustomerName { get; set; }
        public decimal TotalAmount { get; set; }
        public int QuoteId { get; set; }
        // Add/change properties as needed

        // Constructor
        // Add/change as needed
        public Quote(string customerName, decimal totalAmount, int quoteId)
        {
            CustomerName = customerName;
            TotalAmount = totalAmount;
            QuoteId = quoteId;
        }
    }
}
