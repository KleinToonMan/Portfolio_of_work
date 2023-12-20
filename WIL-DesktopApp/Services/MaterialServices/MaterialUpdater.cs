using System.Linq;
using System.Threading.Tasks;
using WIL_DesktopApp.DataModels.DbContexts;
using WIL_DesktopApp.Models;
using MaterialDTO = WIL_DesktopApp.DataModels.Material;

namespace WIL_DesktopApp.Services.MaterialServices
{
    public class MaterialUpdater : IMaterialUpdater
    {
        private readonly IKryptonDbContextFactory _kryptonDbContextFactory;
        /// <summary>
        /// Service for updating materials in the database
        /// </summary>
        /// <param name="kryptonDbContextFactory"></param>
        public MaterialUpdater(IKryptonDbContextFactory kryptonDbContextFactory)
        {
            _kryptonDbContextFactory = kryptonDbContextFactory;
        }

        /// <summary>
        /// Given the old model to be replaced, and the new model, this function replaces the old with the new in the database
        /// </summary>
        /// <param name="oldMaterial"></param>
        /// <param name="newMaterial"></param>
        public void UpdateMaterial(Material oldMaterial, Material newMaterial)
        {

            using (KryptonDbContext context = _kryptonDbContextFactory.CreateKryptonDbContext())
            {
                MaterialDTO oldMaterialDTO = context.Materials.Single<MaterialDTO>(m => m.MaterialId == oldMaterial.Id);
                oldMaterialDTO.MaterialId = newMaterial.Id;
                oldMaterialDTO.Price = newMaterial.UnitPrice;
                oldMaterialDTO.Name = newMaterial.Name;
                oldMaterialDTO.Description = newMaterial.Description;

                context.SaveChanges();
            }
        }

        /// <summary>
        /// Given the old model and new model, this method asynchronously replaces the old with the new in the database
        /// </summary>
        /// <param name="oldMaterial"></param>
        /// <param name="newMaterial"></param>
        /// <returns></returns>
        public async Task UpdateMaterialAsync(Material oldMaterial, Material newMaterial)
        {

            using (KryptonDbContext context = _kryptonDbContextFactory.CreateKryptonDbContext())
            {
                MaterialDTO oldMaterialDTO = context.Materials.Single<MaterialDTO>(m => m.MaterialId == oldMaterial.Id);
                oldMaterialDTO.MaterialId = newMaterial.Id;
                oldMaterialDTO.Price = newMaterial.UnitPrice;
                oldMaterialDTO.Name = newMaterial.Name;
                oldMaterialDTO.Description = newMaterial.Description;

                await context.SaveChangesAsync();
            }
        }
    }
}
