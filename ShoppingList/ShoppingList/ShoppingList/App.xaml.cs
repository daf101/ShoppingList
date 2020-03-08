using ShoppingList.Helpers;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingList
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new MainPage();
            if (Constants.LOGON_REQUIRED)
            {
                MainPage = new NavigationPage(new UserLoginPage());
            }
            else
            {
                MainPage = new NavigationPage(new MainPage());
            }
            
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
