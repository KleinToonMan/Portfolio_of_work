using System;
using System.Windows.Input;

namespace WIL_DesktopApp.Commands
{
    /*
     * Base abstract class for all commands to be used in ViewModels
     */
    public abstract class CommandBase : ICommand
    {
        // Event triggered when the command's ability to execute changes
        public event EventHandler? CanExecuteChanged;

        // Determines if the command can execute, overridden by derived classes
        public virtual bool CanExecute(object? parameter)
        {
            throw new NotImplementedException();
        }

        // Abstract method to execute the command, to be implemented in derived classes
        public abstract void Execute(object? parameter);

        // Raises the CanExecuteChanged event
        protected void OnExecutedChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
