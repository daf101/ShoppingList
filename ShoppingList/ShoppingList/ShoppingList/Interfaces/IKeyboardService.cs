using System;
using Xamarin.Forms;

namespace ShoppingList.Interfaces
{
    public interface IKeyboardService
    {
        bool checkKeyboard();

        void ShowKeyboard();

        void HideKeyboard();
    }
}
