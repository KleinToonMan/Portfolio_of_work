using System.Threading.Tasks;
using WIL_DesktopApp.Models;

namespace WIL_DesktopApp.Services.KryptonUserServices
{
    public interface IKryptonUserRemover
    {
        void RemoveUser(KryptonUser user);
        Task RemoveUserAsync(KryptonUser user);
    }
}
