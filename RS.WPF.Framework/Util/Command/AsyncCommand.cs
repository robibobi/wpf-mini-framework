using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RS.WPF.Framework.Command
{

    // Artikel von Stephen Cleary: https://msdn.microsoft.com/en-us/magazine/dn630647.aspx

    class AsyncCommand : AsyncCommandBase
    {
        readonly Func<CancellationToken, Task> _command;
        readonly Func<bool> _canExecuteFunc;
        readonly CancelAsyncCommand _cancelCommand;

        public AsyncCommand(Func<CancellationToken, Task> command)
        {
            _command = command;
            _canExecuteFunc = () => true;
            _cancelCommand = new CancelAsyncCommand();
        }

        public AsyncCommand(Func<CancellationToken, Task> command, Func<bool> canExecute)
        {
            _command = command;
            _canExecuteFunc = canExecute;
            _cancelCommand = new CancelAsyncCommand();
        }

        public override bool CanExecute(object _)
        {
            return _canExecuteFunc() && (Execution == null || Execution.IsCompleted);
        }

        public override async Task ExecuteAsync(object _)
        {
            _cancelCommand.NotifyCommandStarting();
            Execution = new NotifyTaskCompletion(_command(_cancelCommand.Token));
            OnPropertyChanged(nameof(Execution));
            RaiseCanExecuteChanged();
            await Execution.TaskCompletion;
            _cancelCommand.NotifyCommandFinished();
            RaiseCanExecuteChanged();
        }

        public ICommand CancelCommand => _cancelCommand;


        public NotifyTaskCompletion Execution { get; private set; }
    }


    class AsyncCommand<TParam> : AsyncCommandBase
    {
        readonly Func<CancellationToken, TParam, Task> _command;
        readonly Func<TParam, bool> _canExecuteFunc;
        readonly CancelAsyncCommand _cancelCommand;

        public AsyncCommand(Func<CancellationToken, TParam, Task> command)
        {
            _command = command;
            _canExecuteFunc = _ => true;
            _cancelCommand = new CancelAsyncCommand();
        }

        public AsyncCommand(Func<CancellationToken, TParam, Task> command, Func<TParam, bool> canExecute)
        {
            _command = command;
            _canExecuteFunc = canExecute;
            _cancelCommand = new CancelAsyncCommand();
        }

        public override bool CanExecute(object parameter)
        {
            return _canExecuteFunc(SaveCast(parameter)) && (Execution == null || Execution.IsCompleted);
        }

        public override async Task ExecuteAsync(object parameter)
        {
            TParam param = SaveCast(parameter);
            _cancelCommand.NotifyCommandStarting();
            Execution = new NotifyTaskCompletion(_command(_cancelCommand.Token, param));
            OnPropertyChanged(nameof(Execution));
            RaiseCanExecuteChanged();
            await Execution.TaskCompletion;
            _cancelCommand.NotifyCommandFinished();
            RaiseCanExecuteChanged();
        }

        public ICommand CancelCommand => _cancelCommand;

        public NotifyTaskCompletion Execution { get; private set; }

        private TParam SaveCast(object param)
        {
            if(param == null)
                return default(TParam);
            return (TParam)param;
        }

    }


    class AsyncResultCommand<TResult> : AsyncCommandBase
    {
        readonly Func<CancellationToken, Task<TResult>> _command;
        readonly CancelAsyncCommand _cancelCommand;
        NotifyTaskCompletion<TResult> _execution;

        public AsyncResultCommand(Func<CancellationToken, Task<TResult>> command)
        {
            _command = command;
            _cancelCommand = new CancelAsyncCommand();
        }

        public override bool CanExecute(object parameter)
        {
            return Execution == null || Execution.IsCompleted;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _cancelCommand.NotifyCommandStarting();
            Execution = new NotifyTaskCompletion<TResult>(_command(_cancelCommand.Token));
            OnPropertyChanged("Execution");
            RaiseCanExecuteChanged();
            await Execution.TaskCompletion;
            _cancelCommand.NotifyCommandFinished();
            RaiseCanExecuteChanged();
        }

        public ICommand CancelCommand => _cancelCommand;

        public NotifyTaskCompletion<TResult> Execution { get; private set; }
    }



    class AsyncResultCommand<TParam, TResult> : AsyncCommandBase
    {
        readonly Func<CancellationToken, TParam, Task<TResult>> _command;
        readonly CancelAsyncCommand _cancelCommand;

        public AsyncResultCommand(Func<CancellationToken, TParam, Task<TResult>> command)
        {
            _command = command;
            _cancelCommand = new CancelAsyncCommand();
        }

        public override bool CanExecute(object parameter)
        {
            return Execution == null || Execution.IsCompleted;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            TParam param = (TParam)parameter;
            _cancelCommand.NotifyCommandStarting();
            Execution = new NotifyTaskCompletion<TResult>(_command(_cancelCommand.Token, param));
            OnPropertyChanged("Execution");
            RaiseCanExecuteChanged();
            await Execution.TaskCompletion;
            _cancelCommand.NotifyCommandFinished();
            RaiseCanExecuteChanged();
        }

        public ICommand CancelCommand => _cancelCommand;

        public NotifyTaskCompletion<TResult> Execution { get; private set; }
    }



    internal sealed class CancelAsyncCommand : ICommand
    {
        private CancellationTokenSource _cts = new CancellationTokenSource();
        private bool _commandExecuting;

        public CancellationToken Token { get { return _cts.Token; } }

        public void NotifyCommandStarting()
        {
            _commandExecuting = true;
            if (!_cts.IsCancellationRequested)
                return;
            _cts = new CancellationTokenSource();
            RaiseCanExecuteChanged();
        }

        public void NotifyCommandFinished()
        {
            _commandExecuting = false;
            RaiseCanExecuteChanged();
        }

        bool ICommand.CanExecute(object parameter)
        {
            return _commandExecuting && !_cts.IsCancellationRequested;
        }

        void ICommand.Execute(object parameter)
        {
            _cts.Cancel();
            RaiseCanExecuteChanged();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        private void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }

}
