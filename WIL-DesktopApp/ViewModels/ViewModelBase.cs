using System.ComponentModel;

namespace WIL_DesktopApp.ViewModels
{
    /// <summary>
    /// This is the abstract base class to be inherited from in all view mode 
    /// It ensures that all viewmodels can make use of the propertychanged event
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName) // Notifies whenever a property is changed, so that bindings can update
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
