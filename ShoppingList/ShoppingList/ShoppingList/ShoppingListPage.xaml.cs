using ShoppingList.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace ShoppingList
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShoppingListPage : ContentPage
    {
        public ShoppingListPage()
        {
            InitializeComponent();
            // Stops the ListView from going into the Status Bar:
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            
            shoppingListRefreshView.Command = new Command(() => {
                refresh();
                shoppingListRefreshView.IsRefreshing = false;
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            refresh();
        }

        private async void refresh()
        {
            var items = await Item.GetItems();
            int amountOfItems = items.Count;
            if(items.Count == 0)
            {
                itemListView.IsVisible = false;
                emptyView.IsVisible = true;
            } else
            {
                itemListView.IsVisible = true;
                emptyView.IsVisible = false;
                itemListView.ItemsSource = items;
            }
            
        }

        private void itemListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedItem = itemListView.SelectedItem as Item;
            if (selectedItem != null)
            {
                Navigation.PushAsync(new ItemDetailPage(selectedItem));
            }
        }
    }
}