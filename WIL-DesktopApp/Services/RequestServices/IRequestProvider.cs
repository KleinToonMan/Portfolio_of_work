using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIL_DesktopApp.Models;

namespace WIL_DesktopApp.Services.RequestServices
{
    public interface IRequestProvider
    {
        Task<IEnumerable<Request>> GetAllRequestsAsync();
        Task<Request> GetFullRequestAsync(Request req);

        IEnumerable<Request> GetAllRequests();

        Request GetFullRequest(Request req);

        double GetCurrentMarkup();
    }
}