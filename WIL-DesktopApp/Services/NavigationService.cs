using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WIL_DesktopApp.Stores;

namespace WIL_DesktopApp.Services
{
    public static class NavService
    {

        // Current displayed content (either Window or UserControl)
        private static object? _currentContent;

        // Container to host the UserControls
        private static readonly Grid _navigationContainer;

        // Store for UserStore
        private static UserStore? _userStore;

        private static List<Window> _openedWindows = new List<Window>();

        public static UserStore _UserStore
        {
            get { return _userStore; }
            set { _userStore = value; }
        }

        /// <summary>
        /// Private constructor initializes the NavigationService.
        /// </summary>
        static NavService()
        {
            // Get the main window's navigation container
            _navigationContainer = GetNavigationContainerFromMainWindow();
        }

        /// <summary>
        /// Navigates to a new Window or updates the current grid with a UserControl, handling visibility changes.
        /// </summary>
        /// <param name="content">The Window or UserControl to navigate to or update with.</param>
        /// <param name="userStore">Optional UserStore to pass.</param>
        public static void Navigate(object content, UserStore? userStore = null)
        {
            // Check if the content is a Window
            if (content is Window window)
            {
                // Check if a window of the same type is already open
                if (_openedWindows.Any(w => w.GetType() == window.GetType()))
                {
                    MessageBox.Show("Window is already open!");
                    return;
                }

                // Add the new window to the list of opened windows
                _openedWindows.Add(window);

                // Handle window closing to remove it from the list
                window.Closed += (s, args) => { _openedWindows.Remove(window); };

                // Show the new Window
                window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                window.Show();

                // Close the previous Window if any
                if (_currentContent is Window currentWindow)
                    currentWindow.Close();

                _currentContent = window;
            }
            else if (content is UserControl userControl)
            {
                // Hide the current content, if any
                if (_currentContent != null)
                {
                    if (_currentContent is Window currentWindow)
                        currentWindow.Close();
                    else if (_currentContent is UserControl currentUserControl)
                        currentUserControl.Visibility = Visibility.Collapsed;
                }

                // Show the new UserControl
                userControl.Visibility = Visibility.Visible;
                _currentContent = userControl;

                // Update the navigation container to display the new UserControl
                _navigationContainer.Children.Clear();
                _navigationContainer.Children.Add(userControl);
            }
            else
            {
                throw new ArgumentException("Content must be either a Window or UserControl.");
            }

            // Set the UserStore
            
            if (userStore != null)
            {
                _userStore = userStore;
            }
        }

        /// <summary>
        /// Retrieves the navigation container from the main window.
        /// </summary>
        /// <returns>The navigation container.</returns>
        private static Grid GetNavigationContainerFromMainWindow()
        {
            // Get the main window instance
            MainWindow? mainWindow = Application.Current.MainWindow as MainWindow;

            // Return the navigation container from the main window
            return mainWindow!.NavigationContainer;
        }
    }
}