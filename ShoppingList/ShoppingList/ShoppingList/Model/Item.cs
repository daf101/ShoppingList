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

        public Item() {}
        public Item(int id, string name, int active) 
        {
            this.id = id;
            this.name = name;
            this.active = active;
        }
        public Item(string name)
        {
            this.name = name;
        }

        public async static Task<List<Item>> GetItems(bool sortByName) 
        {
            List<Item> items = new List<Item>();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    //client.Timeout = TimeSpan.FromSeconds(30);
                    var response = await client.GetAsync(Constants.GET_ALL_ITEMS);
                    var json = await response.Content.ReadAsStringAsync();
                    var itemRoot = JsonConvert.DeserializeObject<ItemRoot>(json);
                    int itemSize = items.Count;

                    List<Item> filteredItems;
                    if (sortByName)
                    {
                        //filteredItems = itemRoot.items.Where(item => item.active.Equals(1)).ToList();
                        filteredItems = itemRoot.items.Where(item => item.active.Equals(1)).OrderBy(item => item.name).ToList();
                    } 
                    else
                    {
                        filteredItems = itemRoot.items.Where(item => item.active.Equals(1)).ToList();
                    }
                   
                    items = filteredItems as List<Item>;
                }
            } 
            catch(Exception e)
            {
                items.Add(new Item(e.Message));
            }
            return items;
        }

        public static async Task<HttpResponseMessage> Put(Item item)
        {
            using (HttpClient client = new HttpClient())
            {
                string jsonString;
                string uri;
                if (item.id == 0)
                {
                    uri = Constants.ITEM + item.name;
                    jsonString = JsonConvert.SerializeObject(new { active = item.active });
                    
                }
                else
                {
                    uri = Constants.ITEM + item.id;
                    jsonString = JsonConvert.SerializeObject(new { name = item.name, active = item.active });
                }
                
                HttpContent httpContent = new StringContent(jsonString);
                httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                var response = await client.PutAsync(uri, httpContent);
                return response;

            }
        }

        public static async Task<bool> CanConnect()
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(Constants.GET_ALL_ITEMS);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }

    public class Response
    {
        public IList<Item> items { get; set; }
    }

    public class ItemRoot
    {
        public IList<Item> items { get; set; }
    }
}
