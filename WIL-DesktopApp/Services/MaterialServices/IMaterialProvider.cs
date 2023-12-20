using System.Collections.Generic;
using System.Threading.Tasks;
using WIL_DesktopApp.Models;

namespace WIL_DesktopApp.Services.MaterialServices
{
    public interface IMaterialProvider
    {
        Task<IEnumerable<Material>> GetAllMaterialsAsync();

        IEnumerable<Material> GetAllMaterials();
    }
}
