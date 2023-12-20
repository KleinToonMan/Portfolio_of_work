using System.Windows;

namespace WIL_DesktopApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //Embedded connection string for now
        /*private const string connectionString = "Server=localhost;User ID=root;Password=;Database=krypton_db;";

        private readonly User user;
        private readonly UserStore userStore;
        private readonly KryptonDbContextFactory kryptonDbContextFactory;

        public App()
        {
            //Added database logic to where it will be used in the future, does not get used atm
            ///Commented out for now until we make use of it
            kryptonDbContextFactory = new KryptonDbContextFactory(connectionString);
            IMaterialCreator matCreator = new MaterialCreator(kryptonDbContextFactory);
            IMaterialProvider matProvider = new MaterialProvider(kryptonDbContextFactory);
            IMaterialRemover matRemover = new MaterialRemover(kryptonDbContextFactory);
            IMaterialUpdater matUpdater = new MaterialUpdater(kryptonDbContextFactory);

            MaterialRepository materialRepository = new MaterialRepository(matCreator, matUpdater, matRemover, matProvider);

            user = new User("TestUser", "TestName", "TestLastname", 1, "test@gmail.com", 1, materialRepository);
            userStore = new UserStore(user);
        }*/
    }
}