using System;
using System.Windows.Input;

namespace WpfApp5
{
    internal class ButtonCommand : ICommand
    {
        //public event EventHandler CanExecuteChanged;

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        Action<object> action;
        Func<object, bool> canExecute;
        public ButtonCommand(Action<object> action, Func <object,bool> canExecute)
        {
            this.action = action;
            this.canExecute = canExecute;

        }


        public bool CanExecute(object parameter)
        {
            return canExecute!= null || canExecute(parameter); ; 
        }

        public void Execute(object parameter)
        {
            action(parameter);
        }
    }
}
