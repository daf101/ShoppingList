using ShoppingList.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ShoppingList.ViewModel.Commands
{
    public class NavigationCommand : ICommand
    {
        public MainVM MainVM { get; set; }
        public ShoppingListVM ShoppingListVM { get; set; }

        private int Mode;

        public NavigationCommand(MainVM mainVM)
        {
            MainVM = mainVM;
        }
        public NavigationCommand(ShoppingListVM shoppingListVM)
        {
            ShoppingListVM = shoppingListVM;
        }

        public NavigationCommand(ShoppingListVM shoppingListVM, int mode)
        {
            ShoppingListVM = shoppingListVM;
            Mode = mode;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (MainVM != null)
            {
                MainVM.Navigate();
            }
            else if (ShoppingListVM != null & Mode == 1)
            {
                ShoppingListVM.newItemFABSelected();
            }
            else if (ShoppingListVM != null)
            {
                var item = (Item)parameter;
                ShoppingListVM.item_Selected();
            }
        }
    }
}
