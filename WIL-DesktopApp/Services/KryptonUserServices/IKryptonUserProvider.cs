using System.Collections.Generic;
using System.Threading.Tasks;
using WIL_DesktopApp.Models;

namespace WIL_DesktopApp.Services.KryptonUserServices
{
    public interface IKryptonUserProvider
    {
        Task<IEnumerable<KryptonUser>> GetAllKryptonUsersAsync();
        List<KryptonUser> GetAllKryptonUsers();
    }
}
