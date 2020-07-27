using Plugin.Toast;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using ShoppingList.Helpers;
using ShoppingList.Interfaces;
using ShoppingList.Model;
using ShoppingList.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingList
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPage : PopupPage
    {
        Item SelectedItem;
        ItemDetailVM viewModel;
        public ItemDetailPage(Item selectedItem)
        {
            InitializeComponent();
            SelectedItem = selectedItem;
            viewModel = new ItemDetailVM();
            BindingContext = viewModel;

            // When below message is recieved, close page:
            MessagingCenter.Subscribe<App>((App)Application.Current, Constants.CLOSE_ITEM_DETAIL_PAGE, (sender) =>
            {
                Navigation.PopPopupAsync();
            });

            // When below message is recieved, refocus keyboard
            MessagingCenter.Subscribe<App>((App)Application.Current, Constants.REFOCUS_KEYBOARD, (sender) =>
            {
                Thread.Sleep(1000);
                
                var keyboard = DependencyService.Get<IKeyboardService>();
                keyboard.ShowKeyboard();
            });

            //Navigation.RemovePopupPageAsync(ShoppingListPageUndoPopup);
            //Navigation.RemovePage(ShoppingListPageUndoPopup)
        }
        protected override void OnAppearing()
        {
            // Bringing focus to item name entry:
            refocus();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            // Letting other instances of Popup page know that this one will close so they can re-adjust margins
            MessagingCenter.Send<App>((App)Application.Current, Constants.POPUP_PAGE_FINISHED);
        }

        private void refocus()
        {
            itemNameEntry.Text = SelectedItem.Name;
            itemIdLabel.Text = SelectedItem.Id.ToString();
            itemNameEntry.Focus();
            int itemNameEntryLength = itemNameEntry.Text.Length;
            itemNameEntry.CursorPosition = itemNameEntryLength;
        }
    }
}