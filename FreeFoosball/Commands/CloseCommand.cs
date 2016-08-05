using System;
using System.Windows.Input;
using FreeFoosball.ViewModels;

namespace FreeFoosball.Commands
{
    public class CloseCommand : ICommand
    {
        private readonly Action _onExecute;

        public CloseCommand(Action onExecute)
        {
            _onExecute = onExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _onExecute();
        }
    }
}
