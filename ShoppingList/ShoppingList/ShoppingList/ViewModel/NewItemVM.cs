using ShoppingList.Model;
using ShoppingList.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ShoppingList.ViewModel
{
    public class NewItemVM : INotifyPropertyChanged
    {
        public NewItemCommand NewItemCmd { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        private Item Item;

        public NewItemVM()
        {
            NewItemCmd = new NewItemCommand(this);
            Item = new Item();
        }

        public async void PutNewItem(Item item)
        {
            try
            {
                Item.Put(item);
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Failure", "Item failed to be insert", "Ok");
            }
           
        }
    }


}
