using System.Windows.Controls;
using System.Windows;
using WIL_DesktopApp.Models;
using WIL_DesktopApp.Stores;
using WIL_DesktopApp.DataModels.DbContexts;
using WIL_DesktopApp.Services.MaterialServices;
using WIL_DesktopApp.Services.RequestServices;
using System.Collections.Generic;
using System.Linq;
using WIL_DesktopApp.Services;

namespace WIL_DesktopApp.Views
{
    /// <summary>
    /// Interaction logic for Request.xaml
    /// </summary>
    public partial class DashboardRequestItem : UserControl
    {
        private  UserStore _userStore;
        private  IEnumerable<Material> _materials;
        private List<string> _materialNames;
        //Dont think we need the description?
        private List<string> _materialDescriptions;
        private List<double> _materialUnitPrices;
        private Request _request;

        public DashboardRequestItem(Request req)
        {
            InitializeComponent();
            requestID.Text = req.RequestId.ToString();
            email.Text = req.Email.ToString();
            notes.Text = req.Notes.ToString();
            fileURL.Text = req.FileURL.ToString();

            _request = req;


            //BELOW CODE SHOULD BE IN APP, USER MUST BE SET AFTER LOGIN
            _userStore = NavService._UserStore;

            _materials = _userStore.Materials;
            _materialNames = _materials.Select(mat => mat.Name).ToList();
            _materialUnitPrices = _materials.Select(mat => mat.UnitPrice).ToList();
            _materialDescriptions = _materials.Select(mat => mat.Description).ToList();
        }

        private void LoadDetails(object sender, RoutedEventArgs e)
        {
            FullRequest fr = new(_userStore.GetFullRequest(_request));
            NavService.Navigate(fr);
        }

        private void DeleteRequest(object sender, RoutedEventArgs e)
        {
            var userStore = NavService._UserStore;
            MessageBoxResult result = MessageBox.Show("Are you sure you want to remove this request?","Warning",MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                userStore.RemoveRequest(_request);

                Dashboard db = new Dashboard(userStore.GetUser());
                NavService.Navigate(db, userStore);
            }
        }
    }
}
