using ShoppingList.ViewModel;
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
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(false)]
    public partial class MainPage : TabbedPage
    {
        MainVM viewModel;
        public MainPage()
        {
            InitializeComponent();

            var assembly = typeof(MainPage);

            viewModel = new MainVM();
            BindingContext = viewModel;
            
        }
    }
}
