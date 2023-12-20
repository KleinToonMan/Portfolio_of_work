using System.Collections.Generic;

namespace WIL_DesktopApp.Models
{
    /*
     * <summary>
     * Model of a request that composes of multiple products, the files uploaded by the customer and contact details of the customer
     * </summary>
     */
    public class Request
    {
        public int RequestId { get; set; } // Integer ID of the request, as stated in the DB
        public string FileURL { get; set; } // URL to find all uploaded files of the customer
        public string Notes { get; set; } // Notes associated with order
        public string Email { get; set; } // Contact email of customer
        public IEnumerable<RequestItem> RequestItems { get; set; }
        /// <summary>
        /// Request consisting of products requested by customer as well as neccessary info
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="fileURL"></param>
        /// <param name="email"></param>
        /// <param name="notes"></param>
        public Request(int requestId, string fileURL, string email, IEnumerable<RequestItem> requestItems, string notes = "")
        {
            RequestId = requestId;
            FileURL = fileURL;
            Notes = notes;
            Email = email;
            RequestItems = requestItems;
        }

        /// <summary>
        /// Light version of request, with no fileurl or notes
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="email"></param>
        public Request(int requestId, string email, IEnumerable<RequestItem> requestItems)
        {
            RequestId = requestId;
            Email = email;
            RequestItems = requestItems;
            FileURL = "";
            Notes = "";
        }
    }
}
