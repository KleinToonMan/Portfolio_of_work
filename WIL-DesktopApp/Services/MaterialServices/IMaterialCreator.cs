using System.Threading.Tasks;
using WIL_DesktopApp.Models;
namespace WIL_DesktopApp.Services.MaterialServices
{
    public interface IMaterialCreator
    {
        Task CreateMaterialAsync(Material material);

        void CreateMaterial(Material material);
    }
}
