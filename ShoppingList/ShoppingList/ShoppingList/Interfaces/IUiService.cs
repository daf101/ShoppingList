using ShoppingList.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.Interfaces
{
    public interface IUiService
    {
        void ShowSnackBar(string message, int duration, string action, Action<object> callback);
        void ShowSnackBar(string message, int duration);
    }
}
