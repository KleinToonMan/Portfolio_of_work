using System.Threading.Tasks;
using WIL_DesktopApp.DataModels.DbContexts;
using WIL_DesktopApp.Models;
using MaterialDTO = WIL_DesktopApp.DataModels.Material;

namespace WIL_DesktopApp.Services.MaterialServices
{

    public class MaterialCreator : IMaterialCreator
    {

        private readonly IKryptonDbContextFactory _kryptonDbContextFactory;

        /// <summary>
        /// Service for adding materials to a database
        /// </summary>
        /// <param name="kryptonDbContextFactory"></param>
        public MaterialCreator(IKryptonDbContextFactory kryptonDbContextFactory)
        {
            _kryptonDbContextFactory = kryptonDbContextFactory;
        }

        /// <summary>
        /// Adds a material to the database given a material model
        /// </summary>
        /// <param name="material"></param>
        public void CreateMaterial(Material material)
        {
            using (KryptonDbContext context = _kryptonDbContextFactory.CreateKryptonDbContext())
            {
                MaterialDTO materialDTO = MaterialDTOConverter.ToMaterialDTO(material);
                context.Materials.Add(materialDTO);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Asynchronously adds a material to the database given a material model
        /// </summary>
        /// <param name="material"></param>
        /// <returns></returns>
        public async Task CreateMaterialAsync(Material material)
        {
            using (KryptonDbContext context = _kryptonDbContextFactory.CreateKryptonDbContext())
            {
                MaterialDTO materialDTO = MaterialDTOConverter.ToMaterialDTO(material);
                context.Materials.Add(materialDTO);
                await context.SaveChangesAsync();
            }
        }
    }
}
