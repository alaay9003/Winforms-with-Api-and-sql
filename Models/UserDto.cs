using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Models
{
    public class UserDto
    {
        [JsonProperty("username")]
        public string username { get; set; } 
        [JsonProperty("password")]
        public string password { get; set; }
        [JsonProperty("email")]
        public string email { get; set; }
        [JsonProperty("imageAsBase64")]
        public byte[] Img { get; set; }
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("intrestId")]
        public int intrestId { get; set; }
    }
}
