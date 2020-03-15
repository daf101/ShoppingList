using Plugin.Toast;
using Rg.Plugins.Popup.Extensions;
using ShoppingList.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingList
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        Item SelectedItem;
        public ItemDetailPage(Item selectedItem)
        {
            InitializeComponent();
            SelectedItem = selectedItem;
        }

        protected override void OnAppearing()
        {
            itemNameEntry.Text = SelectedItem.name;
            itemNameEntry.Focus();
            int itemNameEntryLength = itemNameEntry.Text.Length;
            itemNameEntry.CursorPosition = itemNameEntryLength;

            // Get Metrics
            //var width = EditItemStackLayout.Width;
            //var updateButtonWidth = updateButton.Width;
            //EditItemStackLayout.WidthRequest = width;
            //itemNameEntry.WidthRequest = width - updateButtonWidth;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Send<App>((App)Application.Current, "ItemDetailPageFinished");
        }

        private async void deleteButton_Clicked(object sender, EventArgs e)
        {
            Item item = new Item();
            item.active = 0;
            item.name = SelectedItem.name;
            HttpResponseMessage response = await Item.Put(item);

            if (response.StatusCode.ToString() == "OK")
            {
                await Navigation.PopPopupAsync();
                CrossToastPopUp.Current.ShowToastSuccess("Item Removed Successfully");
            }
        }

        private void cancelButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopPopupAsync();
        }

        private void itemNameEntry_Completed(object sender, EventArgs e)
        {
            // Update item
            Navigation.PopPopupAsync();
        }

        private void updateButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopPopupAsync();
        }
    }
}