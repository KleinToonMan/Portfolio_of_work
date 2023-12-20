namespace WIL_DesktopApp.Models
{
    /*
     * <summary>
     * Model to store information about a Material, related to DB table "Material"
     * </summary>
     */
    public class Material
    {
        public int Id { get; set; } // Integer relating to Material ID in DB
        public string Name { get; set; } // Name of material
        public string Description { get; set; } // Description of material
        public double UnitPrice { get; set; } // Price per unit (e.g. 1 m squared) of material
        /// <summary>
        /// Model of a material consisting of an id, name, description and unitPrice
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="unitPrice"></param>
        public Material(int id, string name, string description, double unitPrice)
        {
            Id = id;
            Name = name;
            Description = description;
            UnitPrice = unitPrice;
        }
    }
}
