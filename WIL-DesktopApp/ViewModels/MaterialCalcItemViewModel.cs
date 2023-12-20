using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using WIL_DesktopApp.DataModels.DbContexts;
using WIL_DesktopApp.Models;
using WIL_DesktopApp.Services;
using WIL_DesktopApp.Services.MaterialServices;
using WIL_DesktopApp.Services.RequestServices;
using WIL_DesktopApp.Stores;

namespace WIL_DesktopApp.ViewModels
{
    /// <summary>
    /// ViewModel for managing material calculations.
    /// Provides properties for material names and unit prices.
    /// Utilizes change tracking for efficient data binding.
    /// </summary>
    public class MaterialCalcItemViewModel : ViewModelBase
    {
        //Create the properties that the developers will use in the front end and throughout the program.
        //Userstore needs to be passed through the constructer from the Login.xaml and LoginViewModel.cs.

        //Also has the properties for the material names, descriptions, and prices for the front end.
        private readonly UserStore _userStore;
        private readonly IEnumerable<Material> _materials;
        private List<string> _materialNames;
        //Dont think we need the description?
        private List<string> _materialDescriptions;
        private List<double> _materialUnitPrices;

        //Getter and setters for material names
        public IEnumerable<string> GetMatNames => _materialNames!;
        public void SetMatNames(string matName)
        {
            _materialNames!.Add(matName);
            OnPropertyChanged(nameof(GetMatNames)); // Notify that the collection property has changed
        }
        //Getter and setters for unit prices names
        public IEnumerable<double> GetUnitPrice => _materialUnitPrices!;
        public void SetUnitPrice(double matName)
        {
            _materialUnitPrices!.Add(matName);
            OnPropertyChanged(nameof(GetUnitPrice)); // Notify that the collection property has changed
        }
        public MaterialCalcItemViewModel()
        {
            //Theses three lines need to be deleted once the DB is working properly.
            _materialNames = new List<string>();
            _materialDescriptions = new List<string>();
            _materialUnitPrices = new List<double>();

            _userStore = NavService._UserStore;

            _materials = _userStore.Materials;
            _materialNames = _materials.Select(mat => mat.Name).ToList();
            _materialUnitPrices = _materials.Select(mat => mat.UnitPrice).ToList();
            _materialDescriptions = _materials.Select(mat => mat.Description).ToList();
        }



        /* 
         * This method facilitates property assignment with change tracking. 
         * It updates the property's value, triggers change events, and returns whether the value changed. 
         * Useful for efficient data binding. 
         */
        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string? propertyName = null)
        {
            if (!Equals(field, newValue))
            {
                field = newValue;
                OnPropertyChanged(propertyName!);
                return true;
            }

            return false;
        }



    }
}
