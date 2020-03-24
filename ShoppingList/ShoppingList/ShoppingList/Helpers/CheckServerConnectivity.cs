using ShoppingList.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.Helpers
{
    public class CheckServerConnectivity
    {
        public async Task<bool> checkServerConnectivity ()
        {
            List<Item> items = await Item.GetItems("Default");

            return true;
        }
        
    }
    
}
