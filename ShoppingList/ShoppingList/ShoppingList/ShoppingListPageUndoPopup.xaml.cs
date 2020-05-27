using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingList
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShoppingListPageUndoPopup : PopupPage
    {
        public ShoppingListPageUndoPopup()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var currentPopups = Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopupStack;
            int margin = currentPopups.Count * 45;
            PopupPageStackLayout.Margin = new Thickness(0, 0, 0, margin);
            InformationLabel.Text = currentPopups.Count.ToString();

        }

        private void UndoButton_Clicked(object sender, EventArgs e)
        {
            Navigation.RemovePopupPageAsync(this);
        }
    }
}