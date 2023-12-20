using System.Threading.Tasks;
using WIL_DesktopApp.Models;

namespace WIL_DesktopApp.Services.MaterialServices
{
    public interface IMaterialRemover
    {
        Task RemoveMaterialAsync(Material material);
        void RemoveMaterial(Material material);
    }
}
