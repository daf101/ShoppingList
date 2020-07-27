using Plugin.Toast;
using ShoppingList.Helpers;
using ShoppingList.Model;
using ShoppingList.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ShoppingList.ViewModel
{
    public class MainVM
    {
        public NavigationCommand NavCommand { get; set; }
        public SortByCommand SortByCmd { get; set; }
        public SelectCommand SelectCmd { get; set; }

        public MainVM()
        {
            NavCommand = new NavigationCommand(this);
            SortByCmd = new SortByCommand(this);
            SelectCmd = new SelectCommand(this);
        }
        public async void Navigate()
        {
            await App.Current.MainPage.Navigation.PushAsync(new NewItemPage());
        }

        public async void sortBy()
        {

            string action = await App.Current.MainPage.DisplayActionSheet(Strings.SORT_BY, Strings.CANCEL, null, Constants.SORT_BY_DEFAULT, Constants.SORT_BY_NAME);

            if (action == Constants.SORT_BY_DEFAULT)
            {
                MessagingCenter.Send<App>((App)Application.Current, Constants.SORT_BY_DEFAULT_SELECTED);
            }
            else if (action == Constants.SORT_BY_NAME)
            {
                MessagingCenter.Send<App>((App)Application.Current, Constants.SORT_BY_NAME_SELECTED);
            }

        }

        public void select()
        {
            // Need to tell list view to change mode:
            MessagingCenter.Send<App>((App)Application.Current, Constants.SELECT_MODE_TOGGLED);
        }
    }


}
