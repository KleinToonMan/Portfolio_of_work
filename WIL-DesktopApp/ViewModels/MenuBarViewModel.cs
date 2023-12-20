using WIL_DesktopApp.Models;

namespace WIL_DesktopApp.ViewModels
{

    /// <summary>
    /// Class for menubar, this will house the buttons for the logic across the dashboard.
    /// </summary>
    class MenuBarViewModel : ViewModelBase
    {
        //Empty, still need to implement some logic to verify the user that the admin is logged in so that a material editor button can appear
        public MenuBarViewModel(User user)
        {
            //User needs to get verified here, need a data binding for buttons to ensure that they can be enabled.
        }
    }
}
