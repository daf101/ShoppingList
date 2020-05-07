using Plugin.Toast;
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

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            HttpResponseMessage response;
            try
            {
                Item item = new Item();
                item.Active = 1;
                item.Name = itemEntry.Text;
                response = await Item.Put(item);
                if (response.StatusCode.ToString() == "OK")
                {
                    // Great, the item was added. Letting the user know and going back to previous page:
                    await Navigation.PopPopupAsync();
                    CrossToastPopUp.Current.ShowToastSuccess(Strings.ITEM_INSERTED_SUCCESSFULLY);

                }
            }
            catch
            {
                CrossToastPopUp.Current.ShowToastError(Strings.CANT_CONNECT);
            }



        }
    }
}