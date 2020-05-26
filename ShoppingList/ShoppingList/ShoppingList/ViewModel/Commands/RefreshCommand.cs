using ShoppingList.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;

namespace ShoppingList.ViewModel.Commands
{
    public class RefreshCommand : ICommand
    {
        public ShoppingListVM ViewModel { get; set; }
        public event EventHandler CanExecuteChanged;

        public RefreshCommand(ShoppingListVM viewModel)
        {
            ViewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {

            _ = ViewModel.refresh(Preferences.Get(Constants.SORT_BY, Constants.SORT_BY_DEFAULT));
            
        }
    }
}
