using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingList.ViewModel
{
    public class MainVM
    {
        public async void Navigate()
        {
            await App.Current.MainPage.Navigation.PushAsync(new NewItemPage());
        }
    }

    
}
