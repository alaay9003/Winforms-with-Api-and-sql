using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Test.Models;
using System.Web.Http;
using Newtonsoft.Json.Linq;


namespace Test.Controllers
{
    public class NoteController : ApiController
    {
        private static readonly string baserUrl = "https://noteapi.popssolutions.net/notes/";

        public static async Task<List<NotesDto>> GetAll()
        {
            HttpClient client = new HttpClient();
            string response = await client.GetStringAsync(baserUrl+ "getall");
            List<NotesDto> objResponse1 = JsonConvert.DeserializeObject<List<NotesDto>>(response);
            return objResponse1;
        }
        public static async Task<string> update(NotesDto note)
        {
            using (HttpClient client = new HttpClient())
            {
                // Create the request body
                var requestBody = new NotesDto
                {
                    Id = 2,
                    Text = "test",
                    UserId = 1,
                    PlaceDateTime = "2021-11-18T09:39:44"
                };
                // Convert the request body to JSON
                var jsonBody = Newtonsoft.Json.JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(jsonBody, System.Text.Encoding.UTF8, "application/json");
                // Send the POST request with the request body
                HttpResponseMessage response = await client.PostAsync("https://noteapi.popssolutions.net/notes/update", content);
                return response.ToString();
            }
        }

    }
}

