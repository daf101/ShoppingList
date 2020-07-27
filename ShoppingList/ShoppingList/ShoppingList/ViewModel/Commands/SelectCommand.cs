using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ShoppingList.ViewModel.Commands
{
    public class SelectCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public MainVM ViewModel { get; set; }

        public SelectCommand(MainVM mainVM)
        {
            ViewModel = mainVM;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ViewModel.select();
        }
    }
}
