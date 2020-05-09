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
    public class ItemDetailVM : INotifyPropertyChanged
    {

        public DeleteItemCommand DeleteItemCommand { get; set; }
        public ClosePopUpCommand ClosePopUpCommand { get; set; }
        public UpdateItemCommand UpdateItemCommand { get; set; }
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

        public ItemDetailVM()
        {
            DeleteItemCommand = new DeleteItemCommand(this);
            UpdateItemCommand = new UpdateItemCommand(this);
            ClosePopUpCommand = new ClosePopUpCommand(this);
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

        // Methods go here:
        public async void updateItem(Item updatedItem)
        {
            updatedItem.Active = 1;
            HttpResponseMessage response = await Item.Put(updatedItem);

            if (response.StatusCode.ToString() == "OK")
            {
                CrossToastPopUp.Current.ShowToastSuccess(Strings.ITEM_UPDATED_SUCCESSFULLY);
                //Close popup page:
                MessagingCenter.Send<App>((App)Application.Current, Constants.CLOSE_ITEM_DETAIL_PAGE);
            } else
            {
                // Error occured updating item:

            }
        }

        public async void deleteItem(Item itemToDelete)
        {
            itemToDelete.Active = 0;
            HttpResponseMessage response = await Item.Put(itemToDelete);

            if (response.StatusCode.ToString() == "OK")
            {
                
                CrossToastPopUp.Current.ShowToastSuccess(Strings.ITEM_REMOVED_SUCCESSFULLY);
                //Close popup page:
                MessagingCenter.Send<App>((App)Application.Current, Constants.CLOSE_ITEM_DETAIL_PAGE);
            }
            else
            {
                // Error occured deleting item:

            }
        }

        public void closePopup()
        {
            MessagingCenter.Send<App>((App)Application.Current, Constants.CLOSE_ITEM_DETAIL_PAGE);
        }


    }
}
