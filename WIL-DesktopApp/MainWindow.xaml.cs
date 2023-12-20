using MySqlX.XDevAPI.Common;
using System.ComponentModel;
using System.Windows;
using WIL_DesktopApp.DataModels.DbContexts;
using WIL_DesktopApp.Models;
using WIL_DesktopApp.Services;
using WIL_DesktopApp.Services.KryptonUserServices;
using WIL_DesktopApp.Services.MaterialServices;
using WIL_DesktopApp.Services.RequestServices;
using WIL_DesktopApp.Stores;
using WIL_DesktopApp.Views;

namespace WIL_DesktopApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen; 
            LoadingWindow lw = new();
            lw.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            lw.Show();

            /*IKryptonDbContextFactory kryptonDbContextFactory = new KryptonDbContextFactory("Server = localhost; uid = root; pwd=; database = krypton_db");

            IMaterialCreator materialCreator = new MaterialCreator(kryptonDbContextFactory);
            IMaterialProvider materialProvider = new MaterialProvider(kryptonDbContextFactory);
            IMaterialRemover materialRemover = new MaterialRemover(kryptonDbContextFactory);
            IMaterialUpdater materialUpdater = new MaterialUpdater(kryptonDbContextFactory);

            IRequestProvider requestProvider = new RequestProvider(kryptonDbContextFactory);
            IRequestRemover requestRemover = new RequestRemover(kryptonDbContextFactory);

            IKryptonUserProvider userProvider = new KryptonUserProvider(kryptonDbContextFactory);
            IKryptonUserCreator userCreator = new KrytonUserCreator(kryptonDbContextFactory);
            IKryptonUserRemover userRemover = new KryptonUserRemover(kryptonDbContextFactory);

            RequestRepository reqRepo = new RequestRepository(requestProvider, requestRemover);
            MaterialRepository matRepo = new MaterialRepository(materialCreator, materialUpdater, materialRemover, materialProvider);
            UserRepository userRepository = new UserRepository(userCreator, userProvider, userRemover);

            User test = new User("Login", "Login", "Login", "Login", 0, matRepo, reqRepo);

            UserStore userStore = new UserStore(test);*/


            InitializeComponent();

            NavService.Navigate(new Login());
            lw.Hide();
            Closing += MainWindow_Closing;
        }
        private void MainWindow_Closing(object? sender, CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to quit the application?", "Quit KryptonSigns Desktop", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
            {
                e.Cancel = true; // Cancel the window closing
            }
            else
            {
                Application.Current.Shutdown();
            }
        }
    }
}
