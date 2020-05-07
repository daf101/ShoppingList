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
        public event EventHandler CanExecuteChanged;

        public NewItemCommand(NewItemVM newItemVM)
        {
            viewModel = newItemVM;
        }

        public bool CanExecute(object parameter)
        {
            var item = (Item)parameter;

            if (item != null)
            {
                if (string.IsNullOrEmpty(item.Name))
                {
                    return false;
                }

                if (item.Name != null)
                {
                    return true;
                }


                return false;
            }
            return false;
        }

        public void Execute(object parameter)
        {
            var item = (Item)parameter;
            item.Active = 1;
            viewModel.PutNewItem(item);
        }
    }
}
