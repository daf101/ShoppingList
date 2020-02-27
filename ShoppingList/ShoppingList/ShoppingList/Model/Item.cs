using Newtonsoft.Json;
using ShoppingList.Helpers;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;

namespace ShoppingList.Model
{
    public class Item
    {
        public int id { get; set; }
        public string name { get; set; }
        public int active { get; set; }

        public async static Task<List<Item>> GetItems()
        {
            List<Item> items = new List<Item>();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(Constants.GET_ALL_ITEMS);
                    var json = await response.Content.ReadAsStringAsync();

                    var itemRoot = JsonConvert.DeserializeObject<ItemRoot>(json);

                    items = itemRoot.items as List<Item>;
                }
            } catch(Exception e)
            {
                await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Error", e.Message, "Ok");
            }
            

            return items;
        }
    }

    public class Response
    {
        public IList<Item> items { get; set; }
    }

    public class ItemRoot
    {
        //public Response response { get; set; }
        public IList<Item> items { get; set; }
    }
}
