using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingList
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            
        }

        private void Add_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Alert!", "You Clicked the add button", "Ok");
        }
    }
}
