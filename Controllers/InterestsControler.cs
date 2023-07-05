using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Test.Models;

namespace Test.Controllers
{
    public class InterestsControler
    {
        private static readonly string baserUrl = "https://noteapi.popssolutions.net/intrests/getall";

        public static async Task<List<InterstDto>> GetAll()
        {
            HttpClient client = new HttpClient();
            string response = await client.GetStringAsync("https://noteapi.popssolutions.net/intrests/getall");
            List <InterstDto> objResponse1 = JsonConvert.DeserializeObject<List<InterstDto>>(response);
            return objResponse1;
        }

    }
}
