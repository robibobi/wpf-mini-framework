using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RS.WPF.Framework
{
    public class BindableCommand : ICommand
    {

        private bool mCanExecute;

        private Action mCodeToExecute;

        public BindableCommand(Action codeToExecute, bool canExecute = true)
        {
            mCanExecute = canExecute;

            mCodeToExecute = codeToExecute;
        }

        public event EventHandler CanExecuteChanged;

        public void SetCanExecute(bool canExecute)
        {
            mCanExecute = canExecute;
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, new EventArgs());
        }

        public bool CanExecute(object parameter)
        {
            return mCanExecute;
        }

        public void Execute(object parameter)
        {
            if (CanExecute(null))
            {
                mCodeToExecute();
            }
        }
    }


    public class BindableCommand<T> : ICommand
    {

        private bool mCanExecute = false;

        private Action<T> mCodeToExecute;

        private Func<T, bool> mCanExecuteFunc;

        public BindableCommand(Action<T> codeToInvoke)
        {
            mCodeToExecute = codeToInvoke;
        }
        public BindableCommand(Action<T> codeToExecute, bool canExecute)
        {
            mCodeToExecute = codeToExecute;
            mCanExecute = canExecute;
        }
        public BindableCommand(Action<T> codeToExecute, Func<T, bool> canExecFunc)
        {
            mCodeToExecute = codeToExecute;
            mCanExecuteFunc = canExecFunc;
        }
        public bool CanExecute(object parameter)
        {
            if (mCanExecuteFunc != null)
                return mCanExecute || mCanExecuteFunc((T)parameter);
            return mCanExecute;
        }

        public void SetCanExecute(bool canExecute)
        {
            mCanExecute = canExecute;
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, new EventArgs());
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            mCodeToExecute((T)parameter);
        }
    }

}
