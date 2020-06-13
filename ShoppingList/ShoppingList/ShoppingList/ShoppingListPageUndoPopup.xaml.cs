using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using ShoppingList.Helpers;
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
        Item ClosedItem;
        public ShoppingListPageUndoPopup(Item closedItem)
        {
            InitializeComponent();
            ClosedItem = closedItem;
            // Evaluating if margin needs adjusting to cater for removing white space:
            MessagingCenter.Subscribe<ShoppingListPageUndoPopup, string>(this, Constants.UNDO_POPUP_CLOSED, (sender, arg) => {
                int closedPopupPosition = int.Parse(arg);
                // If the current popup position is greater then the closed popup position, repositioning will be required:
                if (currentPopupPosition > closedPopupPosition)
                {
                    // Re-Adjusting position:
                    currentPopupPosition -= 1;
                    Adjust_Margin(currentPopupPosition);
                }
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var currentPopups = PopupNavigation.Instance.PopupStack;
            currentPopupPosition = currentPopups.Count;
            Adjust_Margin(currentPopupPosition);
            //InformationLabel.Text = currentPopupPosition.ToString();
            InformationLabel.Text = ClosedItem.Name + " deleted.";
        }

        private async void UndoButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.RemovePopupPageAsync(this);


            MessagingCenter.Send(this, Constants.UNDO_POPUP_CLOSED, currentPopupPosition.ToString());
            ClosedItem.Active = 1;
            await Item.Put(ClosedItem);
            MessagingCenter.Send<App>((App)Application.Current, Constants.POPUP_PAGE_FINISHED);
        }

        private void Adjust_Margin(int popupPosition)
        {
            margin = popupPosition * 45;
            PopupPageStackLayout.Margin = new Thickness(0, 0, 0, margin);
        }

        private void Countdown()
        {

        }
    }
}