using Plugin.Toast;
using ShoppingList.Helpers;
using ShoppingList.Model;
using ShoppingList.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;

namespace ShoppingList.ViewModel
{
    public class NewItemVM : INotifyPropertyChanged
    {
        public NewItemCommand NewItemCommand { get; set; }
        
        private Item item;
        public Item Item
        {
            get { return item; }
            set 
            { 
                item = value;
                OnPropertyChanged("Item");
            }
        }

        private int id;
        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                Item = new Item()
                {
                    Id = this.Id,
                    Name = this.Name,
                    Active = this.Active
                };
                OnPropertyChanged("Id");
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set 
            {
                name = value;
                Item = new Item()
                {
                    Id = this.Id,
                    Name = this.Name,
                    Active = this.Active
                };                 
                OnPropertyChanged("Name");
            }
        }

        private int active;
        public int Active
        {
            get { return active; }
            set
            {
                id = value;
                Item = new Item()
                {
                    Id = this.Id,
                    Name = this.Name,
                    Active = this.Active
                };
                OnPropertyChanged("Active");
            }
        }



        public NewItemVM()
        {
            NewItemCommand = new NewItemCommand(this);
            Item = new Item();
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public async void PutNewItem(Item item)
        {
            try
            {
                HttpResponseMessage response = await Item.Put(item);
                //TODO: Send Message to close popup page:
                if (response.StatusCode.ToString() == "OK")
                {
                    // Great, the item was added. Letting the user know and going back to previous page:
                    CrossToastPopUp.Current.ShowToastSuccess(Strings.ITEM_INSERTED_SUCCESSFULLY);

                }
                MessagingCenter.Send<App>((App)Application.Current, Constants.CLOSE_POPUP_PAGE);
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Failure", "Item failed to be insert", "Ok");
            }
           
        }
    }


}
