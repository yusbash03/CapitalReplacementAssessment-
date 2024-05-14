using Newtonsoft.Json;

namespace CapitalReplacementTask.Models
{
    public class Candidates
    {
        [JsonProperty("id")]
        public string id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string dateOfBirth { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string nationality { get; set; }
        public string residence { get; set; }
        public string gender { get; set; }
        public string idNumber { get; set; }
        public bool isDeleted { get; set; }
    }
}
