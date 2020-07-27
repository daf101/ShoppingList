using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
using ShoppingList.Helpers;
using ShoppingList.Model;
using ShoppingList.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ShoppingList.ViewModel
{
    public class ShoppingListVM : INotifyPropertyChanged
    {
        public RefreshCommand RefreshCommand { get; set; }
        public NavigationCommand NavCommand { get; set; }
        public NavigationCommand NewItemNavCommand { get; set; }
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

        private Item selectedItem;

        public Item SelectedItem
        {
            get { return selectedItem; }
            set 
            { 
                if (selectedItem != value)
                {
                    selectedItem = value;
                    item_Selected();
                }
            }
        }


        public ShoppingListVM()
        {
            // Instantiate any Commands/Objects:
            Item = new Item();
            RefreshCommand = new RefreshCommand(this);
            NavCommand = new NavigationCommand(this);
            NewItemNavCommand = new NavigationCommand(this, 1);

        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        // Methods go here:
        public async Task<List<Item>> refresh(string sortBy)
        {
            List<Item> items = await Item.GetItems(sortBy);

            if (items.Count == 0)
            {
                // Display Empty View:
                MessagingCenter.Send<App>((App)Application.Current, Constants.DISPLAY_EMPTY_VIEW);
            }
            else if (items[0].Name.Contains("Unable to resolve host") || items[0].Name.Contains("Unexpected character"))
            {
                // Display network error:
                MessagingCenter.Send<App>((App)Application.Current, Constants.DISPLAY_NETWORK_ERROR);
            }
            else
            {
                // refresh Completed:
                //MessagingCenter.Send<App>((App)Application.Current, Constants.LISTVIEW_REFRESH_COMPLETE);
                MessagingCenter.Send<App,List<Item>>((App)Application.Current, Constants.LISTVIEW_REFRESH_COMPLETE, items);
            }

            return items;
        }

        public async void item_Selected()
        {
            if (selectedItem != null)
            {
                await PopupNavigation.Instance.PushAsync(new ItemDetailPage(selectedItem));
            }
        }

        public async void newItemFABSelected()
        {
            await PopupNavigation.Instance.PushAsync(new NewItemPage());
        }

    }
}
