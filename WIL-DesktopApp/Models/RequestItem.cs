using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using WIL_DesktopApp.DataModels.DbContexts;
using WIL_DesktopApp.Views;

namespace WIL_DesktopApp.Models
{
    public class RequestItem
    {
        public int Id { get; set; } // ID of request item
        public string Name { get; set; } // Name of product derived from base of attribute tree
        public IEnumerable<Attribute> Attributes { get; set; } // Attributes associated with product selected
        public double EstimateGiven { get; set; } // Price given to customer at the time
        public int Quantity { get; set; }
        public double Markup { get; set; }
        /// <summary>
        /// Product requested by user as part of a quote request, filled with attributes
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="attributes"></param>
        public RequestItem(int id, double estimateGiven, IEnumerable<Attribute> attributes, int quantity)
        {
            Name = attributes.First().Name;
            Id = id;
            Attributes = attributes;
            EstimateGiven = estimateGiven;
            Quantity = quantity;
            Markup = 0;
        }
        /// <summary>
        /// Constructor with empty list of attributes, used for dashboard display
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="estimateGiven"></param>
        public RequestItem(int id, double estimateGiven, string name, int quantity)
        {
            Id = id;
            Name = name;
            EstimateGiven = estimateGiven;
            Attributes = Enumerable.Empty<Attribute>();
            Quantity = quantity;
        }

        public double GetQuoteEstimate()
        {
            double estimate = 0;
            var initial = Attributes.First().Values;
            double[] globalValues = initial == null ? Array.Empty<double>() : initial.Values.ToArray();

            

            foreach(Attribute attribute in Attributes.Skip(1))
            {
                estimate += attribute.GetAttributeCost(globalValues);
            }

            return estimate*(1+Markup);
        }
    }
}