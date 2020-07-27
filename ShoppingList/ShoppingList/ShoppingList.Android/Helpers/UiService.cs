using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using Plugin.CurrentActivity;
using ShoppingList.Interfaces;
using ShoppingList.Model;
using Xamarin.Forms;

[assembly: Dependency(typeof(ShoppingList.Droid.Helpers.UiService))]
namespace ShoppingList.Droid.Helpers
{
    public class UiService : IUiService
    {
        public void ShowSnackBar(
            string message, 
            int duration, 
            string actionText, 
            Action<object> action)
        {
            var view = ((Activity)Forms.Context).FindViewById(Android.Resource.Id.Content);
            var snack = Snackbar.Make(view, message, duration);
            if (actionText != null && action != null)
            {
                snack.SetAction(actionText, action);
            }
            snack.Show();
        }

        public void ShowSnackBar(string message, int duration)
        {
            var view = ((Activity)Forms.Context).FindViewById(Android.Resource.Id.Content);
            var snack = Snackbar.Make(view, message, duration);
            snack.Show();
        }
    }
}