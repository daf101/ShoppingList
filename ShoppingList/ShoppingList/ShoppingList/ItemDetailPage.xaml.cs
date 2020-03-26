using Plugin.Toast;
using Rg.Plugins.Popup.Extensions;
using ShoppingList.Helpers;
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
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Send<App>((App)Application.Current, Constants.ITEM_DETAIL_PAGE_FINISHED);
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
                CrossToastPopUp.Current.ShowToastSuccess(Strings.ITEM_REMOVED_SUCCESSFULLY);
            }
        }

        private void cancelButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopPopupAsync();
        }

        private void itemNameEntry_Completed(object sender, EventArgs e)
        {
            // Update item
            updateItem();
            Navigation.PopPopupAsync();
        }

        private void updateButton_Clicked(object sender, EventArgs e)
        {
            updateItem();
            Navigation.PopPopupAsync();
        }

        private async void updateItem()
        {
            SelectedItem.name = itemNameEntry.Text;
            Item test = SelectedItem;
            await Item.Put(SelectedItem);
        }
    }
}