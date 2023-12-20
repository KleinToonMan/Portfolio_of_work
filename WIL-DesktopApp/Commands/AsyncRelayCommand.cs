using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WIL_DesktopApp.Commands
{
    // Asynchronous ICommand implementation for relayed commands
    public class AsyncRelayCommand : ICommand
    {
        private readonly Func<Task> _asyncExecute;
        private readonly Func<bool> _canExecute;

        // Event that triggers when the command's ability to execute changes
        public event EventHandler? CanExecuteChanged;

        // Constructor accepting an async execution function and a can-execute function
        public AsyncRelayCommand(Func<Task> asyncExecute, Func<bool> canExecute)
        {
            _asyncExecute = asyncExecute ?? throw new ArgumentNullException(nameof(asyncExecute));
            _canExecute = canExecute;
        }

        // Determines if the command can execute based on the can-execute function
        public bool CanExecute(object? parameter)
        {
            return _canExecute?.Invoke() ?? true;
        }

        // Executes the command asynchronously
        public async void Execute(object? parameter)
        {
            await ExecuteAsync();
        }

        // Asynchronously executes the command
        public async Task ExecuteAsync()
        {
            if (!CanExecute(null))
                return;

            try
            {
                await _asyncExecute();
            }
            finally
            {
                RaiseCanExecuteChanged();
            }
        }

        // Raises the CanExecuteChanged event
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
