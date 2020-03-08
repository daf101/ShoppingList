using Plugin.Toast;
using ShoppingList.Model;
using ShoppingList.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingList.ViewModel
{
    public class MainVM
    {
        public NavigationCommand NavCommand { get; set; }

        public MainVM()
        {
            NavCommand = new NavigationCommand(this);
        }
        public async void Navigate()
        {
            await App.Current.MainPage.Navigation.PushAsync(new NewItemPage());
        }
    }

    
}
