using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using WIL_DesktopApp.Models;
using WIL_DesktopApp.ViewModels;

namespace WIL_DesktopApp.Views
{
    /// <summary>
    /// Interaction logic for MaterialEditor.xaml
    /// </summary>
    public partial class MaterialEditor : Window
    {
        private MaterialEditorViewModel materialEditViewModel;
        private List<Material> materials;
        public MaterialEditor()
        {
            InitializeComponent();
            materialEditViewModel = new MaterialEditorViewModel();
            materials = materialEditViewModel.GetMaterials();

            for(int x = 0; x < materials.Count; x++)
            {
                LoadOptions(materials[x]);
            }
        }
        public void LoadOptions(Material material)
        {
            MaterialEditorItem item = new MaterialEditorItem(material);
            MaterialList.Children.Add(item);
        }
    }
}
