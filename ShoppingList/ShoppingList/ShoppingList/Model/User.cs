using Newtonsoft.Json;
using ShoppingList.Helpers;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.Model
{
    public class User
    {
        public string Name { get; set; }
        public string AppPassword { get; set; }
        public string AccessToken { get; set; }

        public User(string name, string appPassword, string accessToken)
        {
            Name = name;
            AppPassword = appPassword;
            AccessToken = accessToken;
        }

        

        public static async Task<User> Login(string name, string appPassword)
        {
            HttpResponseMessage response;
            UserLoginResponse userLoginResponse;
            using (HttpClient client = new HttpClient())
            {
                var jsonString = JsonConvert.SerializeObject(new { username = name, password = appPassword });
                HttpContent httpContent = new StringContent(jsonString);
                httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                response = await client.PostAsync(Constants.AUTH, httpContent);
                string jsonResponse = await response.Content.ReadAsStringAsync();
                userLoginResponse = JsonConvert.DeserializeObject<UserLoginResponse>(jsonResponse);
                string accessToken = userLoginResponse.access_token;
                if(response.IsSuccessStatusCode)
                {
                    return new User(name, appPassword, userLoginResponse.access_token);
                } 
                else
                {
                    return new User("loginFailed", "loginFailed", userLoginResponse.description);
                }
            }
            


            
        }
    }

    public class UserLoginResponse
    {
        public string access_token { get; set; }
        public string description { get; set; }
    }
}
