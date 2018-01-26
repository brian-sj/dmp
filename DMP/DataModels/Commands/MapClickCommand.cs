using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DMP.DataModels.Commands
{
    class MapClickCommand : ICommand 
    {

        //private Action _action;
        //private bool _canExecute;

        readonly Action<object> _execute;
        readonly Predicate<object> _canExecute;



        public MapClickCommand(Action<object> execute , Predicate<object> canExecute)
        {

            if (execute == null)
                throw new NullReferenceException("no execute ");

            _execute = execute;
            _canExecute = canExecute;
        }
        public bool CanExecute( object parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public void Execute(object parameter)
        {
            _execute.Invoke( parameter);
        }
    }
}
