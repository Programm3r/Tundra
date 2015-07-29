using System;
using System.Windows.Input;

namespace Tundra.Command
{
    /// <summary>
    /// Delegate Command Class
    /// </summary>
    public class DelegateCommand<TObject> : ICommand
    {
        #region DelegateCommand - Private Fields

        /// <summary>
        /// The can execute predicate
        /// </summary>
        private readonly Predicate<TObject> _canExecute;

        /// <summary>
        /// The execute action
        /// </summary>
        private readonly Action<TObject> _execute;

        #endregion

        #region DelegateCommand - Event Handlers

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        #endregion

        #region DelegateCommand - CTOR

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateCommand{TObject}"/> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        public DelegateCommand(Action<TObject> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateCommand{TObject}"/> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        public DelegateCommand(Action<TObject> execute, Predicate<TObject> canExecute)
        {
            this._execute = execute;
            this._canExecute = canExecute;
        }

        #endregion

        #region DelegateCommand - Virtual Methods

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        /// <returns>
        /// true if this command can be executed; otherwise, false.
        /// </returns>
        public virtual bool CanExecute(object parameter)
        {
            return this._canExecute == null || this._canExecute((TObject) parameter);
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        public virtual void Execute(object parameter)
        {
            this._execute((TObject) parameter);
        }

        #endregion

        #region DelegateCommand - Public Methods

        /// <summary>
        /// Raises the can execute changed.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            if (this.CanExecuteChanged != null)
            {
                this.CanExecuteChanged(this, EventArgs.Empty);
            }
        }

        #endregion
    }
}