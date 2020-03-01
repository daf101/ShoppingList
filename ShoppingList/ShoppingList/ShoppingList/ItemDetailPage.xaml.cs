using ShoppingList.Model;
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
    public partial class ItemDetailPage : ContentPage
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
        }

        private void deleteButton_Clicked(object sender, EventArgs e)
        {
            Item item = new Item();
            item.active = 0;
            item.name = SelectedItem.name;
            Item.Put(item);
        }
    }
}