using Accessibility;
using System;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using WIL_DesktopApp.Models;
using WIL_DesktopApp.Services;

namespace WIL_DesktopApp.Views
{
    /// <summary>
    /// Interaction logic for MaterialEditorItem.xaml
    /// </summary>
    public partial class MaterialEditorItem : UserControl
    {
        private Material _material;
        public MaterialEditorItem(Material material)
        {
            InitializeComponent();
            _material = material;
            MaterialName.Text = _material.Name;
            PriceDisplay.Text = _material.UnitPrice.ToString();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var _userStore = NavService._UserStore;
            double updatedPrice = 0;
            try
            {
                updatedPrice = Double.Parse(PriceUpdateControl.Text);

                Material newMaterial = new Material(_material.Id, _material.Name, _material.Description, updatedPrice);

                _userStore.UpdateMaterial(_material, newMaterial);
                _material = newMaterial;
                PriceDisplay.Text = _material.UnitPrice.ToString();
                MessageBox.Show(_material.Name + " has been updated.", "Material Price Updated", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch
            {
                PriceUpdateControl.Text = "0";
                MessageBox.Show("You can't enter charachters in this field","Error",MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
