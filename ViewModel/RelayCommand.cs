using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Essensausgleich.ViewModel;

/// <summary>
/// Service to Relay UI Events between View and Viemodel
/// </summary>
public class RelayCommand : ICommand
{
#pragma warning disable 1591
    private Action<object> execute;
    private Func<object, bool> canExecute;
    event EventHandler? ICommand.CanExecuteChanged
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
    public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null!)
    {
        this.execute = execute;
        this.canExecute = canExecute;
    }
#pragma warning disable CS8604 // Mögliches Nullverweisargument.
    public bool CanExecute(object? parameter)
    {
       return canExecute == null || canExecute(parameter);
    }

    public void Execute(object? parameter)
    {

        execute(parameter);
#pragma warning restore CS8604 // Mögliches Nullverweisargument.
    }
}
#pragma warning restore 1591

