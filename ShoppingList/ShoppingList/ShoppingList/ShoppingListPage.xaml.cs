﻿using Plugin.Toast;
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
        ShoppingListVM viewModel;

        public ShoppingListPage()
        {
            InitializeComponent();
            // Stops the ListView from going into the Status Bar:
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            var assembly = typeof(ShoppingListPage);
            viewModel = new ShoppingListVM();
            BindingContext = viewModel;

            // Messaging Centre Subscriptions:
            MessagingCenter.Subscribe<App>((App)Application.Current, Constants.POPUP_PAGE_FINISHED, async (sender) =>
            {
                var items = await viewModel.refresh(Preferences.Get(Constants.SORT_BY, Constants.SORT_BY_DEFAULT));
                itemListView.ItemsSource = items;
            });
            MessagingCenter.Subscribe<App>((App)Application.Current, Constants.SORT_BY_DEFAULT_SELECTED, async (sender) =>
            {
                Preferences.Set(Constants.SORT_BY, Constants.SORT_BY_DEFAULT);
                var items = await viewModel.refresh(Preferences.Get(Constants.SORT_BY, Constants.SORT_BY_DEFAULT));
                itemListView.ItemsSource = items;
            });
            MessagingCenter.Subscribe<App>((App)Application.Current, Constants.SORT_BY_NAME_SELECTED, async (sender) =>
            {
                Preferences.Set(Constants.SORT_BY, Constants.SORT_BY_NAME);
                var items = await viewModel.refresh(Preferences.Get(Constants.SORT_BY, Constants.SORT_BY_DEFAULT));
                itemListView.ItemsSource = items;
            });

            MessagingCenter.Subscribe<App>((App)Application.Current,Constants.ITEM_DELETED, async (sender) => {
                await Navigation.PushPopupAsync(new ShoppingListPageUndoPopup());
            });

            // No items in cart, displaying empty view:
            MessagingCenter.Subscribe<App>((App)Application.Current, Constants.DISPLAY_EMPTY_VIEW, (sender) =>
            {
                emptyViewRefreshView.IsVisible = true;
                itemListView.IsVisible = false;
                noInternetRefreshView.IsVisible = false;
                emptyViewRefreshView.IsRefreshing = false;
                itemListView.IsRefreshing = false;
            });

            // Display Network Error:
            MessagingCenter.Subscribe<App>((App)Application.Current, Constants.DISPLAY_NETWORK_ERROR, (sender) =>
            {
                emptyViewRefreshView.IsVisible = false;
                itemListView.IsVisible = false;
                noInternetRefreshView.IsVisible = true;
                CrossToastPopUp.Current.ShowToastError(Strings.UNABLE_TO_CONNECT);
                noInternetRefreshView.IsRefreshing = false;
                itemListView.IsRefreshing = false;
            });

            MessagingCenter.Subscribe<App>((App)Application.Current, Constants.LISTVIEW_REFRESH_COMPLETE, (sender) =>
            {
                itemListView.IsVisible = true;
                noInternetRefreshView.IsVisible = false;
                noInternetRefreshView.IsRefreshing = false;
                emptyViewRefreshView.IsVisible = false;
                itemListView.IsRefreshing = false;
            });
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var items = await viewModel.refresh(Preferences.Get(Constants.SORT_BY, Constants.SORT_BY_DEFAULT));
            itemListView.ItemsSource = items;
            
        }
    }
}