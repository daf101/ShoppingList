using ShoppingList.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ShoppingList.ViewModel.Commands
{
    public class NewItemCommand : ICommand
    {
        NewItemVM viewModel;
        private NewItemVM newItemVM;

        public NewItemCommand(NewItemVM newItemVM)
        {
            viewModel = newItemVM;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            var item = (Item)parameter;
            if (item.name != null)
            {
                return true;
            }
            return false;
        }

        public void Execute(object parameter)
        {
            var item = (Item)parameter;
            viewModel.PutNewItem(item);
        }
    }
}
