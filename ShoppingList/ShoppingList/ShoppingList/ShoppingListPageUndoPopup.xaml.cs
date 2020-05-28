using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using ShoppingList.Helpers;
using System;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingList
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShoppingListPageUndoPopup : PopupPage
    {
        public int currentPopupPosition;
        public int margin;
        public ShoppingListPageUndoPopup()
        {
            InitializeComponent();
            
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
            InformationLabel.Text = currentPopupPosition.ToString();

        }

        private void UndoButton_Clicked(object sender, EventArgs e)
        {
            Navigation.RemovePopupPageAsync(this);
            MessagingCenter.Send(this, Constants.UNDO_POPUP_CLOSED, currentPopupPosition.ToString());
        }

        private void Adjust_Margin(int popupPosition)
        {
            margin = popupPosition * 45;
            PopupPageStackLayout.Margin = new Thickness(0, 0, 0, margin);
        }
    }
}