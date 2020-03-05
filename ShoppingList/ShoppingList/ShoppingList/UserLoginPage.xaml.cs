using Plugin.Toast;
using ShoppingList.Helpers;
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
    public partial class UserLoginPage : ContentPage
    {
        public UserLoginPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
        }

        private void skipLoginButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }

        private async void LoginButton_Clicked_1(object sender, EventArgs e)
        {
            LoginButton.IsVisible = false;
            loginLoading.IsVisible = true;
            User currentUser = await User.Login(emailEntry.Text, passwordEntry.Text);

            if (currentUser.AccessToken == "Invalid credentials")
            {
                CrossToastPopUp.Current.ShowToastError(currentUser.AccessToken);
                LoginButton.IsVisible = true;
                loginLoading.IsVisible = false;
            } 
            else
            {
                await Navigation.PushAsync(new MainPage());
                LoginButton.IsVisible = true;
                loginLoading.IsVisible = false;
                CrossToastPopUp.Current.ShowToastSuccess("Welcome back, " + currentUser.Name.First().ToString().ToUpper() + currentUser.Name.Substring(1));

            }
                    
        }

        private void registerUserButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new UserRegisterPage());
        }
    }
}