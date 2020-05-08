﻿using Plugin.Toast;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using ShoppingList.Helpers;
using ShoppingList.Model;
using ShoppingList.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingList
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewItemPage : PopupPage
    {
        NewItemVM viewModel;
        public NewItemPage()
        {
            InitializeComponent();
            viewModel = new NewItemVM();
            BindingContext = viewModel;

            // When Below Message Recieved, close page.
            MessagingCenter.Subscribe<App>((App)Application.Current, Constants.CLOSE_POPUP_PAGE, (sender) =>
            {
                // Do Stuff
                Navigation.PopPopupAsync();
            });

        }

        protected override void OnAppearing()
        {
            itemEntry.Focus();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Send<App>((App)Application.Current, Constants.POPUP_PAGE_FINISHED);
        }
    }
}