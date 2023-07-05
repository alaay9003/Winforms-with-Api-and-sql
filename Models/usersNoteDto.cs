using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Test.Models
{
    public class usersNoteDto
    {
        [JsonProperty("Id")]
        public int Id { get; set; }
        [JsonProperty("Text")]
        public string Text { get; set; }
        [JsonProperty("UserId")]
        public int? UserId { get; set; }
        [JsonProperty("PlaceDateTime")]
        public string PlaceDateTime { get; set; }
        [JsonProperty("username")]
        public string userName { get; set; }
    }
}
