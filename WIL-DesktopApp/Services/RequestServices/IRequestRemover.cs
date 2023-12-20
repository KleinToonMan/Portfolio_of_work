using System.Threading.Tasks;
using WIL_DesktopApp.Models;

namespace WIL_DesktopApp.Services.RequestServices
{
    public interface IRequestRemover
    {
        Task RemoveRequestAsync(Request request);
        void RemoveRequest(Request request);
    }
}
