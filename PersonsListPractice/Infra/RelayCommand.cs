using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace PersonsListPractice.Infra
{
    public class RelayCommand<T> : ICommand
    {
        #region Fields

        private readonly Action<T> _execute = null;
        private readonly Predicate<T> _canExecute = null;
        private bool _registerForCommandManager;
        protected EventHandler _canExecuteChanged;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand&lt;T&gt;"/> class.
        /// </summary>
        protected RelayCommand()
            : this(null, null, true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        public RelayCommand(Action<T> execute)
            : this(execute, null, false)
        {
        }

        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
            : this(execute, canExecute, true)
        {
        }

        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public RelayCommand(Action<T> execute, Predicate<T> canExecute, bool registerForCommandManager)
        {
            _execute = execute;
            _canExecute = canExecute;
            _registerForCommandManager = registerForCommandManager;
        }

        #endregion Constructors

        #region ICommand Members

        public virtual bool CanExecute(object parameter)
        {
            if (parameter != null)
            {
                return _canExecute == null ? true : _canExecute((T)parameter);
            }
            else
            {
                return _canExecute == null ? true : _canExecute(default(T));
            }
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (_canExecute != null || _registerForCommandManager)
                {
                    if (_registerForCommandManager)
                        CommandManager.RequerySuggested += value;

                    _canExecuteChanged += value;
                }
            }
            remove
            {
                if (_canExecute != null || _registerForCommandManager)
                {
                    if (_registerForCommandManager)
                        CommandManager.RequerySuggested -= value;

                    _canExecuteChanged -= value;
                }
            }
        }

        public virtual void Execute(object parameter)
        {
            _execute((T)parameter);
        }

        #endregion ICommand Members

        public void RaiseCanExecuteChanged()
        {
            OnCanExecuteChanged();
        }

        protected virtual void OnCanExecuteChanged()
        {
            EventHandler canExecuteChangedHandler = _canExecuteChanged;
            if (canExecuteChangedHandler != null)
            {
                Dispatcher dispatcher = Application.Current.Dispatcher;

                if (dispatcher != null && !dispatcher.CheckAccess())
                {
                    dispatcher.BeginInvoke(DispatcherPriority.Normal,
                                           (Action)OnCanExecuteChanged);
                }
                else
                {
                    canExecuteChangedHandler(this, EventArgs.Empty);
                }
            }
        }
    }

    /// <summary>
    /// A command whose sole purpose is to
    /// relay its functionality to other
    /// objects by invoking delegates. The
    /// default return value for the CanExecute
    /// method is 'true'.
    /// </summary>
    public class RelayCommand : ICommand
    {
        #region Fields

        private readonly Action _execute;
        private readonly Func<bool> _canExecute;
        private bool _registerForCommandManager;
        protected EventHandler _canExecuteChanged;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand"/> class.
        /// </summary>
        protected RelayCommand(object removePerson)
            : this(null, null, true)
        {
        }

        /// <summary>
        /// Creates a new command that can always execute.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        public RelayCommand(Action execute)
            : this(execute, null, false)
        {
        }

        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public RelayCommand(Action execute, Func<bool> canExecute)
            : this(execute, canExecute, true)
        {
        }

        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public RelayCommand(Action execute, Func<bool> canExecute, bool registerForCommandManager)
        {
            _execute = execute;
            _canExecute = canExecute;
            _registerForCommandManager = registerForCommandManager;
        }

        #endregion Constructors

        #region ICommand Members

        [DebuggerStepThrough]
        public virtual bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute();
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (_canExecute != null || _registerForCommandManager)
                {
                    if (_registerForCommandManager)
                        CommandManager.RequerySuggested += value;

                    _canExecuteChanged += value;
                }
            }
            remove
            {
                if (_canExecute != null || _registerForCommandManager)
                {
                    if (_registerForCommandManager)
                        CommandManager.RequerySuggested -= value;

                    _canExecuteChanged -= value;
                }
            }
        }

        public virtual void Execute(object parameter)
        {
            _execute();
        }

        #endregion ICommand Members

        public void RaiseCanExecuteChanged()
        {
            OnCanExecuteChanged();
        }

        protected virtual void OnCanExecuteChanged()
        {
            EventHandler canExecuteChangedHandler = _canExecuteChanged;
            if (canExecuteChangedHandler != null)
            {
                Dispatcher dispatcher = Application.Current.Dispatcher;

                if (dispatcher != null && !dispatcher.CheckAccess())
                {
                    dispatcher.BeginInvoke(DispatcherPriority.Normal,
                                           (Action)OnCanExecuteChanged);
                }
                else
                {
                    canExecuteChangedHandler(this, EventArgs.Empty);
                }
            }
        }
    }
}