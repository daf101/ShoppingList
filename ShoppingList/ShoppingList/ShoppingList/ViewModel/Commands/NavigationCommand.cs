using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ShoppingList.ViewModel.Commands
{
    public class NavigationCommand : ICommand
    {
        public MainVM ViewModel { get; set; }

        public NavigationCommand(MainVM mainVM)
        {
            ViewModel = mainVM;
        }
        
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (ViewModel != null)
            {
                ViewModel.Navigate();
            }
        }
    }
}
