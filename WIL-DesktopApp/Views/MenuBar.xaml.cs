using Org.BouncyCastle.Asn1.Ocsp;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WIL_DesktopApp.Models;
using WIL_DesktopApp.Services;
using Request = WIL_DesktopApp.Models.Request;
namespace WIL_DesktopApp.Views
{
    /// <summary>
    /// Interaction logic for MenuBar.xaml
    /// </summary>
    public partial class MenuBar : UserControl
    {
        //Menubar needs to take in userstore to check if it is allowed to be avaliable to the admin
        public MenuBar(int type)
        {
            InitializeComponent();
            //Is admin added material editor button
            IsAdmin(type);
        }

        private void IsAdmin(int userPermissionLevel)
        {
            if (userPermissionLevel == 1)
            {
                MaterialEdit_Button.Visibility = Visibility.Visible;
                UserEdit_Button.Visibility = Visibility.Visible;
            }
        }

        //This needs to be updated to make more sense
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MaterialCalculator materialCalculator = new MaterialCalculator();
            NavService.Navigate(materialCalculator);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MaterialEditor materialEditor = new MaterialEditor();
            NavService.Navigate(materialEditor);
        }

        private void LogOutClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to log out?","Log Out",MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                Login login = new Login();
                NavService.Navigate(login);
            }
        }

        private void UserEditClick(object sender, RoutedEventArgs e)
        {
            UserEditor userEditor = new UserEditor();
            NavService.Navigate(userEditor);
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            List<Request> requests = NavService._UserStore.SearchRequests(SearchBox.Text).ToList();
            Dashboard db = new Dashboard(NavService._UserStore.GetUser());
            db.LoadRequests(requests);

            NavService.Navigate(db);
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            var userStore = NavService._UserStore;
            userStore.RefreshRequests();
            Dashboard db = new Dashboard(userStore.GetUser());
            NavService.Navigate(db, userStore);
        }
    }
}
