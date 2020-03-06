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
        public string username { get; set; }
        public string password { get; set; }
        [JsonIgnore]
        public string access_token { get; set; }

        public User(string username, string password, string access_token)
        {
            this.username = username;
            this.password = password;
            this.access_token = access_token;
        }

        public User(string username, string password)
        {
            this.username = username;
            this.password = password;
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

        public static async Task Register(User newUser)
        {
            HttpResponseMessage response;
            using (HttpClient client = new HttpClient())
            {

                var jsonString = JsonConvert.SerializeObject(newUser);
                HttpContent httpContent = new StringContent(jsonString);
                httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                response = await client.PostAsync(Constants.REGISTER, httpContent);
            }

            return;

        }
    }


    // May need a UserRegisterResponse class, or simply refactor this class to be 
    // "Response" if it can handle both (I'm very sure it can):
    public class UserLoginResponse
    {
        public string access_token { get; set; }
        public string description { get; set; }
    }
}
