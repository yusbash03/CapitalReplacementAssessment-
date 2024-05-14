using Newtonsoft.Json;

namespace CapitalReplacementTask.Models
{
    public class Questions
    {
        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("questionTypeId")]
        public string QuestionTypeId { get; set; } 
        
        [JsonProperty("userId")]
        public string userId { get; set; }

        [JsonProperty("question")]
        public string Question { get; set; }

        //[JsonProperty("answers")]
        //public List<object> Answers { get; set; }

        //[JsonProperty("dateAdded")]
        //public DateTime DateAdded { get; set; }

        public bool isDeleted { get; set; }
    }

    public class Answers
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("answered")]
        public object Answer { get; set; }

        [JsonProperty("candidateId")]
        public string CandidateId { get; set; }

        public bool isDeleted { get; set; }
    }
}
