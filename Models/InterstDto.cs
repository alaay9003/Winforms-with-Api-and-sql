using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Models
{
    public class InterstDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("intrestText")]
        public string intrestText { get; set; }
    }
}
