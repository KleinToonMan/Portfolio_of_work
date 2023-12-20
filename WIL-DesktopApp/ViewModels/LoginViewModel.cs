using System.Threading.Tasks;
using System.Windows;
using WIL_DesktopApp.Commands;
using WIL_DesktopApp.DataModels.DbContexts;
using WIL_DesktopApp.DataModels;
using WIL_DesktopApp.Services;
using WIL_DesktopApp.Services.AuthenticationServices;
using WIL_DesktopApp.Views;
using WIL_DesktopApp.Models;
using WIL_DesktopApp.Services.MaterialServices;
using WIL_DesktopApp.Services.RequestServices;
using WIL_DesktopApp.Stores;
using System.Security;
using System.Windows.Controls;
using System.Runtime.InteropServices;
using System;
using System.Windows.Data;

namespace WIL_DesktopApp.ViewModels
{
    // ViewModel for the login view, handles user authentication
    public class LoginViewModel : ViewModelBase
    {
        private string _userName;
        private string _stringPassword;
        private string _password;
        private const string connectionString = "Server=localhost;Uid=root;Password=;Database=krypton_db;";
        private readonly KryptonDbContextFactory kryptonDbContextFactory;
        private User? authenticatedUser;

        // Property for the username input
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                OnPropertyChanged(nameof(UserName));
                LoginCommand.RaiseCanExecuteChanged();
            }
        }
        public string Password
        {
            get { return _password; }
            set { _password = value;
                OnPropertyChanged(nameof(Password));
                LoginCommand.RaiseCanExecuteChanged();
            }
        }


        // Command to execute the login process
        public AsyncRelayCommand LoginCommand { get; }
        private string _loginButtonString = "Log In";
        public string LoginButtonString 
        {
            get { return _loginButtonString; }
            set
            {   
                _loginButtonString = value;
                OnPropertyChanged(nameof(LoginButtonString));
            } 
        }

        // Constructor
        public LoginViewModel()
        {
            _userName = "";
            _password = null!;
            try
            {
                kryptonDbContextFactory = new KryptonDbContextFactory(connectionString);
            }
            catch
            {
                MessageBox.Show("Could not connect to the database", "Error", MessageBoxButton.OK);
            }
            

            LoginButtonString = "Log In";
            // Initialize the login command and set its execution conditions
            LoginCommand = new AsyncRelayCommand(LoginAsync, () =>
            {
                return !string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password);
            });
        }

        // Asynchronous method to handle login
        private Task LoginAsync()
        {
            LoginButtonString = "Logging in...";
            IAuthenticationService authService = new AuthenticationService(kryptonDbContextFactory);
            authenticatedUser = authService.GetAuthenticatedUser(UserName, Password);
            // Check user authentication and navigate accordingly
            if (authenticatedUser != null)
            {
                NavService._UserStore = new UserStore(authenticatedUser);
                var dashBoard = new Dashboard(authenticatedUser);
                NavService.Navigate(dashBoard);
            }
            else
            {
                LoginButtonString = "Log In";
                MessageBox.Show("Username or password is incorrect", "Incorrect Credentials", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return Task.CompletedTask;
        }

    }
}
