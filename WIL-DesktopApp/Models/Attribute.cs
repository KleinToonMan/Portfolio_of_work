using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIL_DesktopApp.Models
{
    /*
     * <summary>
     * Model of an attribute that can be selected by a user
     * </summary>
     */
    public class Attribute
    {
        public string Name { get; set; } // Name of attribute
        public int Id { get; set; } // ID of attribute as stated in DB
        public double MaterialPrice { get; set; } // Material cost associated with attribute (if any)
        public Dictionary<string, double>? Values { get; set; } // List of values associated with attribute (if any)

        public bool UseGlobalValue { get; set; }
        public Attribute(string name, int id, double materialPrice, double priceModifier, Dictionary<string, double>? values = null)
        {
            Name = name;
            Id = id;
            MaterialPrice = materialPrice * priceModifier;
            Values = values;
            UseGlobalValue = true;
        }

        public double GetAttributeCost(double[] globals)
        {
            double cost = MaterialPrice;
            if (Values != null) {
                foreach (var val in Values)
                {
                    cost *= val.Value;
                }
            }

            if(globals.Length > 0 && UseGlobalValue) {
                foreach(var val in globals)
                {
                    cost *= val;
                }
            }

            return cost;
            
        }

    }
}