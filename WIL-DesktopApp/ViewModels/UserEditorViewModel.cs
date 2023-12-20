using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WIL_DesktopApp.Commands;
using WIL_DesktopApp.Models;
using WIL_DesktopApp.Services;
using WIL_DesktopApp.Views;

namespace WIL_DesktopApp.ViewModels
{
    public class UserEditorViewModel : ViewModelBase
    {
        private List<KryptonUser> users;
        private int userIndex;
        private KryptonUser oldUser;
        private bool isAdmin;
        public AsyncRelayCommand BackCommand { get; set; }
        public AsyncRelayCommand BackToUserEditCommand { get; set; }
        public AsyncRelayCommand UserIndexCommand { get; set; }
        public AsyncRelayCommand UpdateCommand { get; set; }
        public AsyncRelayCommand DeleteCommand { get; set; }
        public UserEditorViewModel()
        {
            users = NavService._UserStore.GetKryptonUsers();
            userIndex = -1;

            BackCommand = new AsyncRelayCommand(navigateBack, () =>
            {
                return true;
            } );

            BackToUserEditCommand = new AsyncRelayCommand(navigateBackToUsrEdit, () =>
            {
                return true;
            });

            UserIndexCommand = new AsyncRelayCommand(showUserInfo, () =>
            {
                if(userIndex < 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            });

            UpdateCommand = new AsyncRelayCommand(updateUser, () =>
            {
                return !string.IsNullOrEmpty(nameof(UserName)) && !string.IsNullOrEmpty(nameof(Name)) && !string.IsNullOrEmpty(nameof(Surname))
                && !string.IsNullOrEmpty(nameof(Email)) && !string.IsNullOrEmpty(nameof(Type));
            });

            DeleteCommand = new AsyncRelayCommand(deleteUser, () =>
            {
                if (userIndex < 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            });
        }
        //Private methods for all the commands
        /// <summary>
        /// Private method the navigation back to the user editor page
        /// </summary>
        /// <returns>Task completed</returns>
        private Task navigateBack()
        {
            var dashBoard = new Dashboard(NavService._UserStore.GetUser());
            NavService.Navigate(dashBoard);
            return Task.CompletedTask;
        }
        /// <summary>
        /// Private method the navigation back to the dashboard
        /// </summary>
        /// <returns>Task completed<returns>
        private Task navigateBackToUsrEdit()
        {
            var userEditor = new UserEditor();
            users[userIndex] = oldUser;
            NavService.Navigate(userEditor);
            return Task.CompletedTask;
        }
        /// <summary>
        /// Private method for the edit user button. Shows the userInfo page
        /// </summary>
        /// <returns>Task completed</returns>
        private Task showUserInfo()
        {
            if(Type == 1)
            {
                isAdmin = true;
            }
            else
            {
                isAdmin = false;
            }
            oldUser = new KryptonUser(UserName,Name,Surname,Email,Type);
            UserInfo userInfo = new UserInfo(this);
            NavService.Navigate(userInfo);
            return Task.CompletedTask;
        }
        /// <summary>
        /// Private method for the update button.
        /// </summary>
        /// <returns></returns>
        private Task updateUser()
        {
            int type = 0;
            if (isAdmin)
            {
                type = 1;
            }
            var newUser = new KryptonUser(UserName, Name, Surname, Email, type);
            if (newUser.Username.Equals(oldUser.Username) && newUser.FirstName.Equals(oldUser.FirstName) && newUser.LastName.Equals(oldUser.LastName) &&
                newUser.Email.Equals(oldUser.Email) && newUser.Type == oldUser.Type)
            {
                MessageBox.Show("No details where changed for user: " + UserName, "Can't update", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {

                if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Surname) && !string.IsNullOrEmpty(Email))
                {
                    var loggedInUser = NavService._UserStore.GetUser();
                    if (loggedInUser.Type == oldUser.Type && loggedInUser.Username.Equals(oldUser.Username))
                    {
                        MessageBox.Show("Can't change authentication level whilest logged into this account", "Can't update", MessageBoxButton.OK, MessageBoxImage.Information);
                        IsAdmin = true;
                    }
                    else
                    {
                        MessageBoxResult result = MessageBox.Show("Are you sure you want to change " + oldUser.Username + "'s details?",
                        "Update Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (result == MessageBoxResult.Yes)
                        {
                            NavService._UserStore.UpdateKryptonUser(oldUser, newUser);
                            MessageBox.Show("User has been updated", "Updated", MessageBoxButton.OK, MessageBoxImage.Information);
                            oldUser = newUser;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Fill in all fields before updating the user", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            return Task.CompletedTask;
        }
        /// <summary>
        /// Deletes a user from the database
        /// </summary>
        /// <returns>Task completion</returns>
        private Task deleteUser()
        {
            var loggedInUser = NavService._UserStore.GetUser();
            if(loggedInUser.Username.Equals(oldUser.Username))
            {
                MessageBox.Show("You cannot remove the logged in user account","Cant Delete",MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete " + oldUser.Username + "?",
                        "Delete Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    NavService._UserStore.RemoveKryptonUser(oldUser);
                    MessageBox.Show("User has been deleted", "User Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                    RefreshUsers();
                    Dashboard dash = new Dashboard(NavService._UserStore.GetUser());
                    NavService.Navigate(dash);
                }
            }
            

            return Task.CompletedTask;
        }
        private void RefreshUsers()
        {
            users.Clear();
            users = NavService._UserStore.GetKryptonUsers();
        }
        public List<KryptonUser> GetUsers 
        {
            get { return users; }
            set { users = value;
            }
        }

        public int UserIndex
        {
            get { return userIndex; }
            set {
                userIndex = value;
                OnPropertyChanged(nameof(UserIndex));
                UserIndexCommand.RaiseCanExecuteChanged();
            }
        }

        public bool IsAdmin
        {
            get 
            {
                return isAdmin;
            }
            set
            {
                isAdmin = value;
                OnPropertyChanged(nameof(IsAdmin));
            }
        }

        public int Type
        {
            get { return users[userIndex].Type; }
            set { users[userIndex].Type = value;
                OnPropertyChanged(nameof(Type));
                UpdateCommand.RaiseCanExecuteChanged();
            }
        }

        public string UserName
        {
            get { return users[userIndex].Username; }
            set { users[userIndex].Username = value;
                OnPropertyChanged(nameof(UserName));
                UpdateCommand.RaiseCanExecuteChanged();
            }
        }

        public string Name
        {
            get { return users[userIndex].FirstName; }
            set { users[userIndex].FirstName = value;
                OnPropertyChanged(nameof(Name));
                UpdateCommand.RaiseCanExecuteChanged();
            }
        }

        public string Surname
        {
            get { return users[userIndex].LastName; }
            set { users[userIndex].LastName = value;
                OnPropertyChanged(nameof(Surname));
                UpdateCommand.RaiseCanExecuteChanged();
            }
        }

        public string Email
        {
            get { return users[userIndex].Email; }
            set { users[userIndex].Email = value;
                OnPropertyChanged(nameof(Email));
                UpdateCommand.RaiseCanExecuteChanged();
            }
        }
    }
}
