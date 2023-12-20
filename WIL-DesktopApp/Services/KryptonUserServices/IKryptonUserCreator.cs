using System.Threading.Tasks;
using WIL_DesktopApp.Models;

namespace WIL_DesktopApp.Services.KryptonUserServices
{
    public interface IKryptonUserCreator
    {
        void CreateUser(KryptonUser user);
        Task CreateUserAsync(KryptonUser user);
    }
}
