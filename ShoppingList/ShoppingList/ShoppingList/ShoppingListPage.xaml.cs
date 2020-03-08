﻿using Plugin.Toast;
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
            if (items.Count == 0)
            {
                itemListView.IsVisible = false;
                emptyView.IsVisible = true;
                noInternetView.IsVisible = false;
            }
            else if (items[0].name.Contains("Unable to resolve host"))
            {
                emptyView.IsVisible = false;
                itemListView.IsVisible = false;
                noInternetView.IsVisible = true;
                CrossToastPopUp.Current.ShowToastError("Unable to connect to server");
            }
            else
            {
                itemListView.IsVisible = true;
                noInternetView.IsVisible = false;
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