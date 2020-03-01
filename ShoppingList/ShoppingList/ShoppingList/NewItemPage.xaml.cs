using ShoppingList.Model;
using ShoppingList.ViewModel;
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
    public partial class NewItemPage : ContentPage
    {
        NewItemVM viewModel;
        public NewItemPage()
        {
            InitializeComponent();
            viewModel = new NewItemVM();
            BindingContext = viewModel;
        }

        private void AddButton_Clicked(object sender, EventArgs e)
        {
            Item item = new Item();
            item.active = 1;
            item.name = itemEntry.Text;
            Item.Put(item);
        }
    }
}