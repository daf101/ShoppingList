using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ShoppingList.Interfaces;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(ShoppingList.Droid.Helpers.OpenAppService))]
namespace ShoppingList.Droid.Helpers
{
    public class OpenAppService : IOpenAppService
    {
        public void OpenOpenVPN()
        {
            Intent intent = Android.App.Application.Context.PackageManager.GetLaunchIntentForPackage("net.openvpn.openvpn");

            intent.AddFlags(ActivityFlags.NewTask);
            Forms.Context.StartActivity(intent);
        }
    }
}