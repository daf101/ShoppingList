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
            itemListView.RefreshCommand = new Command(() => {
                //Do your stuff.
                refresh();
                itemListView.IsRefreshing = false;
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            refresh();
        }

        private async void refresh()
        {
            //itemListView.IsRefreshing = true;
            var items = await Item.GetItems();

            itemListView.ItemsSource = items;
            //itemListView.IsRefreshing = false;
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