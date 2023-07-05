using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Test.Models;
using System.Text.Json;
using System.Collections.Generic;

namespace Test.Controllers
{
    public class userController
    {
        private static readonly string baserUrl = "https://noteapi.popssolutions.net/users/";

        public static async Task<string> AddUser(UserDto user)
        {
       
            HttpClient client = new HttpClient();
            //UserDto objResponse1 = JsonConvert.SerializeObject<UserDto>;
            var json = System.Text.Json.JsonSerializer.Serialize(user);
            StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync( baserUrl + "insert", stringContent);
            return response.ToString();
        }
        public static async Task<List<UserDto>> GetAll()
        {
            HttpClient client = new HttpClient();
            string response = await client.GetStringAsync("https://noteapi.popssolutions.net/users/getall");
            List<UserDto> objResponse = JsonConvert.DeserializeObject<List<UserDto>>(response);
            return objResponse;

        }
    }
}
