using Newtonsoft.Json;

namespace CapitalReplacementTask.Models
{
    public class QuestionTypes
    {
        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("nameOfType")]
        public string nameOfType { get; set; }

        public bool isDeleted { get; set; }
    }
}
