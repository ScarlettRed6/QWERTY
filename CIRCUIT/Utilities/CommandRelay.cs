using System.Windows.Input;

namespace CIRCUIT.Utilities
{
    internal class CommandRelay<T> : ICommand
    {
        private readonly Action<T> _execute;
        private readonly Func<bool> _canExecute;

        public event EventHandler CanExecuteChanged;

        public CommandRelay(Action<T> execute) : this(execute, null) { }

        public CommandRelay(Action<T> execute, Func<bool> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute();
        }

        public void Execute(object parameter)
        {
            if (parameter is T value)
            {
                _execute(value); // Execute the action with the cast parameter
            }
            else if (parameter == null && typeof(T).IsClass)
            {
                _execute(default); // If parameter is null and T is a reference type, call with default value
            }
            else
            {
                throw new InvalidOperationException("Parameter type is not compatible.");
            }
        }

        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

    }
}
