using Plugin.Toast;
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
    public partial class UserRegisterPage : ContentPage
    {
        public UserRegisterPage()
        {
            InitializeComponent();
        }

        private async void RegisterUserButton_Clicked(object sender, EventArgs e)
        {

            User newUser = new User(emailEntry.Text, passwordEntry.Text);
            // If one of the fields is empty, tell the user:
            if (passwordEntry.Text == null || confirmPasswordEntry.Text == null)
            {
                CrossToastPopUp.Current.ShowToastError("Password fields cannot be empty!");
                return;
            }
            // Checking that passwords match:
            if (passwordEntry.Text != confirmPasswordEntry.Text)
            {
                CrossToastPopUp.Current.ShowToastError("Passwords do not match - Try again");
                return;
            }

            RegisterUserButton.IsVisible = false;
            registerLoading.IsVisible = true;
            User isSuccessfull = await User.Register(newUser);

            if (isSuccessfull.username == "registerFailed")
            {
                CrossToastPopUp.Current.ShowToastError(isSuccessfull.access_token);
            } 
            else
            {
                await Navigation.PopAsync();
                CrossToastPopUp.Current.ShowToastSuccess(isSuccessfull.username + " created successfully");
            }
            RegisterUserButton.IsVisible = true;
            registerLoading.IsVisible = false;
            return;

        }
    }
}