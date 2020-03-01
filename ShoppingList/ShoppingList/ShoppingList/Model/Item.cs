using Newtonsoft.Json;
using ShoppingList.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
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

                    int itemSize = items.Count;

                    var filteredItems = itemRoot.items.Where(item => item.active.Equals(1)).ToList();


                    //items = itemRoot.items as List<Item>;
                    items = filteredItems as List<Item>;

                }
            } catch(Exception e)
            {
                await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Error", e.Message, "Ok");
            }
            

            return items;
        }

        public static async void Put(Item item)
        {
            using (HttpClient client = new HttpClient())
            {
                string uri = Constants.ITEM + item.name;
                var jsonString = JsonConvert.SerializeObject(new { active = item.active});
                HttpContent httpContent = new StringContent(jsonString);
                httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                var response = await client.PutAsync(uri, httpContent);
            }
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
