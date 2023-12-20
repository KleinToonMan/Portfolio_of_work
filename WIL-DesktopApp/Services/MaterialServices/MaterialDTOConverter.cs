using Material = WIL_DesktopApp.Models.Material;
using MaterialDTO = WIL_DesktopApp.DataModels.Material;
namespace WIL_DesktopApp.Services.MaterialServices
{
    public static class MaterialDTOConverter
    {
        /// <summary>
        /// Converts a model to a data transfer object (used for database manipulation)
        /// </summary>
        /// <param name="matData"></param>
        /// <returns></returns>
        public static Material ToMaterialModel(MaterialDTO matData)
        {
            return new Material(matData.MaterialId, matData.Name, matData.Description ?? "", matData.Price);
        }
        /// <summary>
        /// Converts data transfer objects to workable model
        /// </summary>
        /// <param name="matModel"></param>
        /// <returns></returns>
        public static MaterialDTO ToMaterialDTO(Material matModel)
        {
            return new MaterialDTO()
            {
                MaterialId = matModel.Id,
                Name = matModel.Name,
                Description = matModel.Description,
                Price = matModel.UnitPrice
            };
        }
    }
}
