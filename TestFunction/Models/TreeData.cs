using Newtonsoft.Json;

namespace TestFunction.Models
{
    public class TreeData
    {
        public string Id { get; set; }
        //[JsonProperty("dataTitle")]
        //[JsonPropertyName("dataTitle")]
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public string ParentId { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public double? Price { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int? Quantity { get; set; }
        public bool Folder { get; set; }
        public bool Lazy { get; set; }
    }
}
