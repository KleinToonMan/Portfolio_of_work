using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using WIL_DesktopApp.Commands;
using WIL_DesktopApp.DataModels.DbContexts;
using WIL_DesktopApp.Models;
using WIL_DesktopApp.Services;
using WIL_DesktopApp.Services.AuthenticationServices;
using WIL_DesktopApp.Services.MaterialServices;
using WIL_DesktopApp.Services.RequestServices;
using WIL_DesktopApp.Stores;
using WIL_DesktopApp.Views;

namespace WIL_DesktopApp.ViewModels
{
    /// <summary>
    /// Class for the MaterialEditorViewModel, this will house the import
    /// </summary>

    public class MaterialEditorViewModel : ViewModelBase
    {
        /* 
         * This method facilitates property assignment with change tracking. 
         * It updates the property's value, triggers change events, and returns whether the value changed. 
         * Useful for efficient data binding. 
         */
        //Also has the properties for the material names, descriptions, and prices for the front end.
        private readonly IEnumerable<Material> _materials;
        //private Material oldMaterial;
        private int _materialID;
        private string _materialName;
        private string _materialDescription;
        private string _materialUnitPrice;
        private UserStore _userStore;

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
        //Returns the list of material names
        public List<Material> GetMaterials()
        {
            return _materials.ToList();
        }
        
        // Property for the updated price input
        public string UpdatedPrice
        {
            get { return _materialUnitPrice; }
            set
            {
                _materialUnitPrice = value;
                OnPropertyChanged(nameof(UpdatedPrice));
                UpdateCommandAsync.RaiseCanExecuteChanged();
            }
        }
        //Gets the material id of a given material
        public int MaterialID
        {
            get
            {
                return _materialID;
            }
            set
            {
                _materialID = value;
                OnPropertyChanged(nameof(MaterialID));
            }
        }
        //Property for MaterialName
        public string MaterialName
        {
            get { return _materialName; }
            set
            {
                _materialName = value;
                OnPropertyChanged(nameof(MaterialName));
            }
        }

        // Command to execute the login process
        public AsyncRelayCommand UpdateCommandAsync { get; }
        //ViewModel Constructor
        public MaterialEditorViewModel()
        {
            _userStore = NavService._UserStore;

            _materialID = 0;
            _materialName = "";
            _materialDescription = "";
            _materialUnitPrice = "0";
            _materials = _userStore.Materials;

            UpdateCommandAsync = new AsyncRelayCommand(UpdateCommand, () =>
            {
                double unitPrice = 0;
                try
                {
                    unitPrice = Double.Parse(_materialUnitPrice);

                    if (unitPrice <= 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                catch 
                {
                    MessageBox.Show("You can't enter characters in this field", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            });
            
        }

        //Update command
        /// <summary>
        /// Updates the prices of materials
        /// </summary>
        /// <returns></returns>
        private Task UpdateCommand()
        {
            //_userStore.UpdateMaterialAsync(oldMaterial, updatedMaterial);
            MessageBox.Show(_materialName);
            return Task.CompletedTask;
        }

        //Returns the list of unit prices
        /*public List<double> GetUnitPrices()
        {
            return _materialUnitPrices;
        }
        //Returns the list of material IDs
        public List<int> GetMaterialIDs()
        {
            return _materialIDs;
        }*/
        
    }
}
