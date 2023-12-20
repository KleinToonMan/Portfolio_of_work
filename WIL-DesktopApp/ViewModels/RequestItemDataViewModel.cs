using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIL_DesktopApp.ViewModels
{
    public class RequestItemDataViewModel
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public string Attributes { get; set; }
        public double EstimateGiven { get; set; }
        public int Quantity { get; set; }
        public double Estimate { get; set; }
    }
}
