using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ShoppingList.ViewModel.Commands
{
    public class ClosePopUpCommand : ICommand
    {

        ItemDetailVM viewModel;
        public event EventHandler CanExecuteChanged;
        public ClosePopUpCommand(ItemDetailVM itemDetailVM)
        {
            viewModel = itemDetailVM;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            viewModel.closePopup();
        }
    }
}
