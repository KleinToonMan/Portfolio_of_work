using System.Collections.Generic;
using System.Threading.Tasks;
using WIL_DesktopApp.Services.MaterialServices;

namespace WIL_DesktopApp.Models
{
    public class MaterialRepository
    {
        private readonly IMaterialCreator _materialCreator;
        private readonly IMaterialUpdater _materialUpdater;
        private readonly IMaterialRemover _materialRemover;
        private readonly IMaterialProvider _materialProvider;

        public MaterialRepository(IMaterialCreator materialCreator, IMaterialUpdater materialUpdater, IMaterialRemover materialRemover, IMaterialProvider materialProvider)
        {
            _materialCreator = materialCreator;
            _materialUpdater = materialUpdater;
            _materialRemover = materialRemover;
            _materialProvider = materialProvider;
        }

        /// <summary>
        /// Gets all materials as material models
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Material>> GetAllMaterialsAsync()
        {
            return await _materialProvider.GetAllMaterialsAsync();
        }

        public IEnumerable<Material> GetAllMaterials()
        {
            return _materialProvider.GetAllMaterials();
        }

        public async Task AddMaterialAsync(Material material)
        {
            await _materialCreator.CreateMaterialAsync(material);
        }

        public void AddMaterial(Material material)
        {
            _materialCreator.CreateMaterial(material);
        }

        public async Task UpdateMaterialAsync(Material oldMaterial, Material newMaterial)
        {
            await _materialUpdater.UpdateMaterialAsync(oldMaterial, newMaterial);
        }

        public void UpdateMaterial(Material oldMaterial, Material newMaterial)
        {
            _materialUpdater.UpdateMaterial(oldMaterial, newMaterial);
        }

        public async Task RemoveMaterialAsync(Material material)
        {
            await _materialRemover.RemoveMaterialAsync(material);
        }

        public void RemoveMaterial(Material material)
        {
            _materialRemover.RemoveMaterial(material);
        }
    }
}
