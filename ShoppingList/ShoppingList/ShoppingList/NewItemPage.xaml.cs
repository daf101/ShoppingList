using Plugin.Toast;
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
    public partial class NewItemPage : ContentPage
    {
        NewItemVM viewModel;
        public NewItemPage()
        {
            InitializeComponent();
            viewModel = new NewItemVM();
            BindingContext = viewModel;
        }

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            HttpResponseMessage response;
            try
            {
                Item item = new Item();
                item.active = 1;
                item.name = itemEntry.Text;
                response = await Item.Put(item);
                if (response.StatusCode.ToString() == "OK")
                {
                    // Great, the item was added. Letting the user know and going back to previous page:
                    await Navigation.PopAsync();
                    CrossToastPopUp.Current.ShowToastSuccess(Strings.ITEM_INSERTED_SUCCESSFULLY);

                }
            } catch
            {
                CrossToastPopUp.Current.ShowToastError(Strings.CANT_CONNECT);
            }
            

            
        }
    }
}