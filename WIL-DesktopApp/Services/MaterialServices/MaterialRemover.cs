using System.Threading.Tasks;
using WIL_DesktopApp.DataModels.DbContexts;
using WIL_DesktopApp.Models;
using MaterialDTO = WIL_DesktopApp.DataModels.Material;

namespace WIL_DesktopApp.Services.MaterialServices
{
    public class MaterialRemover : IMaterialRemover
    {
        private readonly IKryptonDbContextFactory _kryptonDbContextFactory;

        /// <summary>
        /// Service for deleting materials from the database
        /// </summary>
        /// <param name="kryptonDbContextFactory"></param>
        public MaterialRemover(IKryptonDbContextFactory kryptonDbContextFactory)
        {
            _kryptonDbContextFactory = kryptonDbContextFactory;
        }

        /// <summary>
        /// Removes a given material model from the database
        /// </summary>
        /// <param name="material"></param>
        public void RemoveMaterial(Material material)
        {
            using (KryptonDbContext context = _kryptonDbContextFactory.CreateKryptonDbContext())
            {
                MaterialDTO materialDTO = MaterialDTOConverter.ToMaterialDTO(material);

                context.Materials.Remove(materialDTO);

                context.SaveChanges();
            }
        }

        /// <summary>
        /// Asynchronously removes a given material model from the database
        /// </summary>
        /// <param name="material"></param>
        /// <returns></returns>
        public async Task RemoveMaterialAsync(Material material)
        {
            using (KryptonDbContext context = _kryptonDbContextFactory.CreateKryptonDbContext())
            {
                MaterialDTO materialDTO = MaterialDTOConverter.ToMaterialDTO(material);

                context.Materials.Remove(materialDTO);
                await context.SaveChangesAsync();
            };
        }


    }
}
