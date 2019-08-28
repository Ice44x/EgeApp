using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EgeApp
{
    public class RelayCommand : ICommand
    {
        private Action mAction;

        /// <summary>
        /// The event thath fired hen the  <see cref="CanExecute(object)"/> value has changed
        /// </summary>
        public event EventHandler CanExecuteChanged = (sender, e) => { };

        public RelayCommand(Action action)
        {
            mAction = action;
        }

        /// <summary>
        /// A relay command can always execute
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            mAction();
        }
    }
}
