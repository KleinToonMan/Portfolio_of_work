using System.Threading.Tasks;
using WIL_DesktopApp.Models;

namespace WIL_DesktopApp.Services.MaterialServices
{
    public interface IMaterialUpdater
    {
        Task UpdateMaterialAsync(Material oldMaterial, Material newMaterial);

        void UpdateMaterial(Material oldMaterial, Material newMaterial);
    }
}
