using System;
using System.Windows.Input;

namespace VierGewinntWPF
{
    class Command : ICommand
    {
        public event EventHandler CanExecuteChanged;
        protected Action action = null;
        protected Action<Object> parameterizedAction = null;
        private bool canExecute = false;

        public Command(Action action, bool canExecute = true)
        {
            this.action = action;
            this.canExecute = canExecute;
        }

        public Command(Action<object> parameterizedAction, bool canExecute = true)
        {
            this.parameterizedAction = parameterizedAction;
            this.canExecute = canExecute;
        }

        public bool CanExecute
        {
            get { return canExecute; }
            set {
                if (canExecute != value)
                {
                    canExecute = value;
                    EventHandler canExecuteChanged = CanExecuteChanged;
                    if(CanExecuteChanged != null)
                    {
                        canExecuteChanged(this, EventArgs.Empty);
                    }
                }
            }
        }

        public virtual void DoExecute(object param)
        {
            //  Call the action or the parameterized action, whichever has been set.
            InvokeAction(param);
        }

        protected void InvokeAction(object param)
        {
            Action theAction = action;
            Action<object> theParameterizedAction = parameterizedAction;
            if (theAction != null)
                theAction();
            else theParameterizedAction?.Invoke(param);
        }


        bool ICommand.CanExecute(object parameter) { return canExecute; }
        void ICommand.Execute(object parameter) { DoExecute(parameter); }
    }
}
