using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Models
{
    public class NotesDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("userId")]
        public int? UserId { get; set; }
        [JsonProperty("placeDateTime")]
        public String PlaceDateTime { get; set; }

    }
}
