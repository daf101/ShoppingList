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
                //refocus();

                var keyboard = DependencyService.Get<IKeyboardService>();
                keyboard.ShowKeyboard();


            });


        }

        protected override void OnAppearing()
        {
            // Requesting all open undo pops to close:
            try
            {
                //MessagingCenter.Send<App>((App)Application.Current, Constants.REQUEST_UNDO_POPUP_CLOSE);
            }
            catch
            {
                // If there's an issue, not to worry.
            }
            refocus();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

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