using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WIL_DesktopApp.DataModels.DbContexts;
using WIL_DesktopApp.Models;
using MaterialDTO = WIL_DesktopApp.DataModels.Material;

using System.Windows;
using System.Collections;
using System;

namespace WIL_DesktopApp.Services.MaterialServices
{
    public class MaterialProvider : IMaterialProvider
    {
        private readonly IKryptonDbContextFactory _kryptonDbContextFactory;
        public MaterialProvider(IKryptonDbContextFactory kryptonDbContextFactory)
        {
            _kryptonDbContextFactory = kryptonDbContextFactory;
        }
        /// <summary>
        /// Asynchronously provides a list of material models from the database
        /// </summary>
        /// <returns>List of material models</returns>
        public async Task<IEnumerable<Material>> GetAllMaterialsAsync()
        {
            using (KryptonDbContext context = _kryptonDbContextFactory.CreateKryptonDbContext())
            {
                IEnumerable<MaterialDTO> materialdtos = await context.Materials.ToListAsync();

                return materialdtos.Select(r => MaterialDTOConverter.ToMaterialModel(r));
            }
        }

        /// <summary>
        /// Provides a list of material models from the database
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Material> GetAllMaterials()
        {
            using (KryptonDbContext context = _kryptonDbContextFactory.CreateKryptonDbContext())
            {
                IEnumerable<MaterialDTO> materialdtos = Enumerable.Empty<MaterialDTO>();
                try
                {
                    //Tries to query the database
                    materialdtos = context.Materials.ToList();
                }
                ///If the query fails an error message will be displayed to the user
                catch (Exception ex)
                {
                    MessageBox.Show("Database connection failed. " + ex.Message,"Error",MessageBoxButton.OK, MessageBoxImage.Error);
                }
                //Will return an empty emumerable or a fully populated enumerable
                return materialdtos.Select(r => MaterialDTOConverter.ToMaterialModel(r));

                
            }
        }
        /// <summary>
        /// Asynchronously returns a single model from the database based on ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Material> GetMaterialByIdAsync(int id)
        {
            using (KryptonDbContext context = _kryptonDbContextFactory.CreateKryptonDbContext())
            {
                MaterialDTO materialdto = await context.Materials.SingleAsync<MaterialDTO>(a => a.MaterialId == id);
                return MaterialDTOConverter.ToMaterialModel(materialdto);
            }
        }


    }
}
