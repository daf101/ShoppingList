using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using ShoppingList.Helpers;
using ShoppingList.Interfaces;
using ShoppingList.Model;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingList
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShoppingListPageUndoPopup : PopupPage
    {
        public int currentPopupPosition;
        public int margin;
        public int popupOnSwitch;
        Item ClosedItem;
        public ShoppingListPageUndoPopup(Item closedItem)
        {
            InitializeComponent();
            ClosedItem = closedItem;
            // Evaluating if margin needs adjusting to cater for removing white space:
            MessagingCenter.Subscribe<ShoppingListPageUndoPopup, string>(this, Constants.UNDO_POPUP_CLOSED, (sender, arg) =>
            {
                int closedPopupPosition = int.Parse(arg);
                // If the current popup position is greater then the closed popup position, repositioning will be required:
                if (currentPopupPosition > closedPopupPosition)
                {
                    // Re-Adjusting position:
                    currentPopupPosition -= 1;
                    Adjust_Margin(currentPopupPosition);
                }
            });

            MessagingCenter.Subscribe<App>((App)Application.Current, Constants.REQUEST_UNDO_POPUP_CLOSE, (sender) =>
            {

                Navigation.RemovePopupPageAsync(this);
            });

            int count = 4;
            Device.StartTimer(new TimeSpan(0, 0, 1), () =>
            {
                // Do something every 1 second:
                Device.BeginInvokeOnMainThread(() =>
                {
                    undoButton.Text = "(" + count.ToString() + ") Undo";
                    count--;
                });

                if (count == 0 & popupOnSwitch == 1)
                {
                    try
                    {
                        popupOnSwitch = 0;
                        Navigation.RemovePopupPageAsync(this);
                    }
                    catch (Exception e)
                    {
                        // Page removal failed due to already being removed (It's fine, this code runs after the popup is removed for some reason):
                    }
                    // Letting any other other instances of this page know this page is done and to re-evaluate their positions:
                    MessagingCenter.Send(this, Constants.UNDO_POPUP_CLOSED, currentPopupPosition.ToString());
                    return false;
                }
                else
                {
                    return true;
                }
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var currentPopups = PopupNavigation.Instance.PopupStack;
            currentPopupPosition = currentPopups.Count;
            Adjust_Margin(currentPopupPosition);
            InformationLabel.Text = ClosedItem.Name + " deleted successfully.";
            popupOnSwitch = 1;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            if (checkKeyboard())
            {
                MessagingCenter.Send<App>((App)Application.Current, Constants.REFOCUS_KEYBOARD);

            }
        }

        private async void UndoButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.RemovePopupPageAsync(this);
            MessagingCenter.Send(this, Constants.UNDO_POPUP_CLOSED, currentPopupPosition.ToString());
            ClosedItem.Active = 1;
            popupOnSwitch = 0;
            await Item.Put(ClosedItem);
            MessagingCenter.Send<App>((App)Application.Current, Constants.POPUP_PAGE_FINISHED);
        }

        private void Adjust_Margin(int popupPosition)
        {
            margin = popupPosition * 45;
            PopupPageStackLayout.Margin = new Thickness(0, 0, 0, margin);
        }

        private bool checkKeyboard()
        {
            var keyboard = DependencyService.Get<IKeyboardService>();
            return keyboard.checkKeyboard();
        }
    }
}