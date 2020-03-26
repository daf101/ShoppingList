using Plugin.Toast;
using Rg.Plugins.Popup.Extensions;
using ShoppingList.Helpers;
using ShoppingList.Model;
using ShoppingList.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using Application = Xamarin.Forms.Application;

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

            Command refreshCommand = new Command(() =>
            {
                refresh(Preferences.Get(Constants.SORT_BY, Constants.SORT_BY_DEFAULT));
            });

            itemListView.RefreshCommand = refreshCommand;
            noInternetRefreshView.Command = refreshCommand;
            emptyViewRefreshView.Command = refreshCommand;

            MessagingCenter.Subscribe<App>((App)Application.Current, Constants.ITEM_DETAIL_PAGE_FINISHED, (sender) =>
            {
                
                refresh(Preferences.Get(Constants.SORT_BY, Constants.SORT_BY_DEFAULT));
            });
            MessagingCenter.Subscribe<App>((App)Application.Current, Constants.SORT_BY_DEFAULT_SELECTED, (sender) =>
            {
                Preferences.Set(Constants.SORT_BY, Constants.SORT_BY_DEFAULT);
                refresh(Preferences.Get(Constants.SORT_BY, Constants.SORT_BY_DEFAULT));
            });
            MessagingCenter.Subscribe<App>((App)Application.Current, Constants.SORT_BY_NAME_SELECTED, (sender) =>
            {
                Preferences.Set(Constants.SORT_BY, Constants.SORT_BY_NAME);
                refresh(Preferences.Get(Constants.SORT_BY, Constants.SORT_BY_DEFAULT));
            });

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            refresh(Preferences.Get(Constants.SORT_BY, Constants.SORT_BY_DEFAULT));
        }

        public async void refresh(string sortBy)
        {
            var items = await Item.GetItems(sortBy);

            //itemListView.IsRefreshing = true;

            if (items.Count == 0)
            {
                emptyViewRefreshView.IsVisible = true;
                itemListView.IsVisible = false;
                noInternetRefreshView.IsVisible = false;
                emptyViewRefreshView.IsRefreshing = false;
                itemListView.IsRefreshing = false;
            }
            else if (items[0].name.Contains("Unable to resolve host"))
            {
                emptyViewRefreshView.IsVisible = false;
                itemListView.IsVisible = false;
                noInternetRefreshView.IsVisible = true;
                CrossToastPopUp.Current.ShowToastError(Strings.UNABLE_TO_CONNECT);
                noInternetRefreshView.IsRefreshing = false;
                itemListView.IsRefreshing = false;
            }
            else
            {
                itemListView.IsVisible = true;
                noInternetRefreshView.IsVisible = false;
                noInternetRefreshView.IsRefreshing = false;
                emptyViewRefreshView.IsVisible = false;
                itemListView.IsRefreshing = false;
                itemListView.ItemsSource = items;

            }


        }

        private void itemListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedItem = itemListView.SelectedItem as Item;
            if (selectedItem != null)
            {
                Navigation.PushPopupAsync(new ItemDetailPage(selectedItem));
                itemListView.SelectedItem = null; // de-select the row
            }
        }

        private void newItemFab_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NewItemPage());
        }
    }
}